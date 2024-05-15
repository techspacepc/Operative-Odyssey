using InteractionLayerManagement;
using Organs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tags;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VisibilityManager : MonoBehaviour
{
    public enum ManagedObjects
    {
        Torso,
        Scalpel,
        // Due to retrictiveness of enums; them being a compile-time member, I've to write out the Organs from the organ enum here as well.
        Heart,
        Kidney,
        Eye
    }

    private MaterialManager materialManager;

    private Renderer torso, scalpel;

    private XRSocketInteractor traySocket;

    private const int fadeTime = 2;
    private const float _fadeUpdateInterval = 0.1f;
    private readonly WaitForSeconds fadeUpdateInterval = new(_fadeUpdateInterval);
    private const float alphaDecrementor = -1f / (fadeTime / _fadeUpdateInterval);
    private const float alphaIncrementor = 1f / (fadeTime / _fadeUpdateInterval);

    private readonly Coroutine[] fadeCoroutines = new Coroutine[3];

    private bool IsArrayNullOrEmpty(Renderer[] array) => array == null || array.Length == 0;
    private Renderer GetRendererByTag(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<Renderer>();
    private void LogArrayEmptyOrNullWarning(Renderer[] array, string methodName)
        => Debug.LogWarning($"GameObject Fading Array >{array}< was {(array == null ? "null" : "empty")}.\n The {methodName} method has been returned to prevent a {nameof(NullReferenceException)}.");

    private void StartOnCoroutineAvailable(int coroutineIndex, IEnumerator coroutine)
    {
        if (fadeCoroutines[coroutineIndex] != null)
        {
            StopCoroutine(fadeCoroutines[coroutineIndex]);
            fadeCoroutines[coroutineIndex] = null;
        }

        fadeCoroutines[coroutineIndex] = StartCoroutine(coroutine);
    }

    private void OnOrganSocketed(SelectEnterEventArgs args)
    {
        StartOnCoroutineAvailable(0, FadeOutObject(torso));
        StartOnCoroutineAvailable(1, FadeInObject(scalpel));
        StartOnCoroutineAvailable(2, FadeOutObject(XROrganSocketInteractor.idleOrgans.ToArray()));
    }

    private void OnOrganUnsocketed(SelectExitEventArgs args)
    {
        StartOnCoroutineAvailable(0, FadeInObject(torso));
        StartOnCoroutineAvailable(1, FadeOutObject(scalpel));
        StartOnCoroutineAvailable(2, FadeInObject(XROrganSocketInteractor.idleOrgans.ToArray()));
    }

    private void GetFadingVariables(in Renderer[] renderers, out float currentAlpha, out List<Material> materials, out List<Color> colors)
    {
        materials = new();
        colors = new();

        foreach (Renderer renderer in renderers)
        {
            Material material = renderer.GetComponent<Renderer>().material; // Passing in the Material or Renderer as parameter would be more efficient.
            materials.Add(material);
            colors.Add(material.color);

            renderer.GetComponent<XRGrabInteractable>().enabled = material.color.a <= 1;
        }

        currentAlpha = colors[0].a; // Uses colors of index 0 since all objects will have the same alpha anyway - they all fade at the same time.
    }
    private void GetFadingVariables(in Renderer gameObject, out float currentAlpha, out Material material, out Color color)
    {
        material = gameObject.GetComponent<Renderer>().material; // Passing in the Material or Renderer as parameter would be more efficient.
        color = material.color;

        currentAlpha = color.a;
    }

    private float UpdateCurrentAlphaBy(float currentAlpha, float alphaDelta, in List<Material> materials, in List<Color> colors)
    {
        currentAlpha += alphaDelta;

        for (int i = 0; i < materials.Count; i++)
        {
            Color color = colors[i];
            color.a = currentAlpha;
            materials[i].color = color;
        }

        return Mathf.Clamp01(currentAlpha);
    }
    private float UpdateCurrentAlphaBy(float currentAlpha, float alphaDelta, in Material material, Color color)
    {
        currentAlpha += alphaDelta;

        Color col = color;
        col.a = currentAlpha;
        material.color = col;

        return Mathf.Clamp01(currentAlpha);
    }

    private IEnumerator FadeOutObject(Renderer[] renderers)
    {
        if (IsArrayNullOrEmpty(renderers))
        {
            LogArrayEmptyOrNullWarning(renderers, nameof(FadeOutObject));
            yield break;
        }

        GetFadingVariables(in renderers, out float currentAlpha, out List<Material> materials, out List<Color> colors);

        for (int i = 0; i < renderers.Length; i++) // Since this currently only uses the organ list, it assumes ALL objects have the XRGrabInteractable component.
        {
            renderers[i].GetComponent<XRGrabInteractable>().RemoveInteractionLayer(InteractionLayer.Default);

            Material material, opaqueMaterial, managedMaterial;
            opaqueMaterial = material = materials[i];
            managedMaterial = materialManager.managedMaterials[(ManagedObjects)Enum.Parse(typeof(ManagedObjects), renderers[i].GetComponent<IOrgan>().Organ.ToString())];

            material = managedMaterial;
            managedMaterial = opaqueMaterial;
        }

        while (currentAlpha != 0)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaDecrementor, materials, colors);

            yield return fadeUpdateInterval;
        }
    }
    private IEnumerator FadeOutObject(Renderer renderer)
    {
        GetFadingVariables(in renderer, out float currentAlpha, out Material material, out Color color);

        if (renderer.TryGetComponent(out XRGrabInteractable interactable)) interactable.RemoveInteractionLayer(InteractionLayer.Default);

        while (currentAlpha != 0)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaDecrementor, material, color);

            yield return fadeUpdateInterval;
        }
    }

    private IEnumerator FadeInObject(Renderer[] renderers)
    {
        if (IsArrayNullOrEmpty(renderers))
        {
            LogArrayEmptyOrNullWarning(renderers, nameof(FadeInObject));
            yield break;
        }

        GetFadingVariables(in renderers, out float currentAlpha, out List<Material> materials, out List<Color> colors);

        while (currentAlpha != 1)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaIncrementor, materials, colors);

            yield return fadeUpdateInterval;
        }

        for (int i = 0; i < renderers.Length; i++) // Since this currently only uses the organ list, it assumes ALL objects have the XRGrabInteractable component.
        {
            renderers[i].GetComponent<XRGrabInteractable>().AddInteractionLayer(InteractionLayer.Default);

            Material material, opaqueMaterial, managedMaterial;
            opaqueMaterial = material = materials[i];
            managedMaterial = materialManager.managedMaterials[(ManagedObjects)Enum.Parse(typeof(ManagedObjects), renderers[i].GetComponent<IOrgan>().Organ.ToString())];

            material = managedMaterial;
            managedMaterial = opaqueMaterial;
        }
    }
    private IEnumerator FadeInObject(Renderer renderer)
    {
        GetFadingVariables(in renderer, out float currentAlpha, out Material material, out Color color);

        while (currentAlpha != 1)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaIncrementor, material, color);

            yield return fadeUpdateInterval;
        }

        if (renderer.TryGetComponent(out XRGrabInteractable interactable)) interactable.AddInteractionLayer(InteractionLayer.Default);
    }

    private void Awake()
    {
        materialManager = GetComponent<MaterialManager>();

        traySocket = GameObject.FindGameObjectWithTag(Tag.Tray).GetComponent<XRSocketInteractor>();

        torso = GetRendererByTag(Tag.Torso);
        scalpel = GetRendererByTag(Tag.Scalpel);

        StartCoroutine(FadeOutObject(scalpel));
    }

    private void OnEnable()
    {
        traySocket.selectEntered.AddListener(OnOrganSocketed);
        traySocket.selectExited.AddListener(OnOrganUnsocketed);
    }

    private void OnDisable()
    {
        traySocket.selectEntered.RemoveListener(OnOrganSocketed);
        traySocket.selectExited.RemoveListener(OnOrganUnsocketed);
    }
}