using Constants;
using InteractionLayerManagement;
using MessageSuppression;
using Organs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Tags;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This class uses method overloading. Therefore, several methods do have the same name.
/// The difference and reason why it's used here is to differantiate singular objects and arrays of objects for their fading animation.
/// This is important due to performance reasons, but it also benefits flexibility reasons.
/// To not get confused of whether one method is singular or plural, just check for the array indicator [].
/// In general, the singular method gets used for the Scalpel and Torso, and the plural gets used for the Organs.
/// </summary>
public class VisibilityManager : MonoBehaviour
{
    private MaterialManager materialManager;

    private Renderer torso, scalpel;

    private XRSocketInteractor traySocket;

    private const int fadeTime = 2;

    [SuppressMessage(Suppress.Style.Category, Suppress.Style.CheckId, Justification = Suppress.Style.Justification)]
    private const float _fadeUpdateInterval = 0.05f;
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
            Material material = renderer.sharedMaterial;
            if (!material.name.Contains(Const.MaterialTransparent) && material.color.a != 1)
                material.color = new Color(material.color.r, material.color.g, material.color.b, 1);

            materials.Add(material);
            colors.Add(material.color);

            if (material.color.a <= 1)
                renderer.GetComponent<XRGrabInteractable>().RemoveInteractionLayer(InteractionLayer.Default);
        }

        currentAlpha = colors[0].a; // Uses colors of index 0 since all objects will have the same alpha anyway - they all fade at the same time.
    }
    private void GetFadingVariables(in Renderer renderer, out float currentAlpha, out Material material, out Color color)
    {
        material = renderer.sharedMaterial;
        if (!material.name.Contains(Const.MaterialTransparent) && material.color.a != 1)
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1);

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
            if (currentAlpha < 1) break;

            Renderer renderer = renderers[i];
            Material material = materials[i];
            string materialName = materialManager.GetBaseMaterialName(material.name);

            renderer.GetComponent<XRGrabInteractable>().RemoveInteractionLayer(InteractionLayer.Default);

            renderer.sharedMaterial = materials[i] = materialManager.managedMaterials[materialName];
            materialManager.managedMaterials[materialName] = material;
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

        if (currentAlpha == 1)
        {
            Material previousMaterial = material;
            string materialName = materialManager.GetBaseMaterialName(material.name);

            renderer.sharedMaterial = material = materialManager.managedMaterials[materialName];
            materialManager.managedMaterials[materialName] = previousMaterial;
        }

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
            Renderer renderer = renderers[i];
            Material material = materials[i];
            string materialName = materialManager.GetBaseMaterialName(material.name);

            renderer.GetComponent<XRGrabInteractable>().AddInteractionLayer(InteractionLayer.Default);

            renderer.sharedMaterial = materialManager.managedMaterials[materialName];
            materialManager.managedMaterials[materialName] = material;
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

        string materialName = materialManager.GetBaseMaterialName(material.name);

        renderer.sharedMaterial = materialManager.managedMaterials[materialName];
        materialManager.managedMaterials[materialName] = material;

        if (renderer.TryGetComponent(out XRGrabInteractable interactable)) interactable.AddInteractionLayer(InteractionLayer.Default);
    }

    private void Awake()
    {
        materialManager = GetComponent<MaterialManager>();

        traySocket = GameObject.FindGameObjectWithTag(Tag.Tray).GetComponent<XRSocketInteractor>();

        torso = GetRendererByTag(Tag.Torso);
        scalpel = GetRendererByTag(Tag.Scalpel);
    }
    private void Start()
    {
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