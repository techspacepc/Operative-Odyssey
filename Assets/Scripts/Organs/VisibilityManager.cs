using Constants;
using Organs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VisibilityManager : MonoBehaviour
{
    [SerializeField] private GameObject torso, scalpel;

    private Renderer torsoRenderer;
    [SerializeField] private Material torsoTransparant;
    private Material torsoOpaque;

    private XRSocketInteractor traySocket;

    private const int fadeTime = 2;
    private const float _fadeUpdateInterval = 0.1f;
    private readonly WaitForSeconds fadeUpdateInterval = new(_fadeUpdateInterval);
    private const float alphaDecrementor = -1f / (fadeTime / _fadeUpdateInterval);
    private const float alphaIncrementor = 1f / (fadeTime / _fadeUpdateInterval);

    private void OnOrganSocketed(SelectEnterEventArgs args)
    {
        if (traySocket.interactablesSelected.Count == 0) return;

        StartCoroutine(FadeOutObject(torso));
        StartCoroutine(FadeInObject(scalpel));
        StartCoroutine(FadeOutObject(XROrganSocketInteractor.idleOrgans.ToArray())); // This should probably be optimised.
    }

    private void OnOrganUnsocketed(SelectExitEventArgs args)
    {
        if (traySocket.interactablesSelected.Count > 0) return;

        StartCoroutine(FadeInObject(torso));
        StartCoroutine(FadeOutObject(scalpel));
        StartCoroutine(FadeInObject(XROrganSocketInteractor.idleOrgans.ToArray())); // This should probably be optimised.
    }

    private void GetFadingVariables(in GameObject[] gameObjects, out float currentAlpha, out List<Material> materials, out List<Color> colors)
    {
        materials = new();
        colors = new();

        foreach (GameObject gameObject in gameObjects)
        {
            Material material = gameObject.GetComponent<Renderer>().material; // Passing in the Material or Renderer as parameter would be more efficient.
            materials.Add(material);
            colors.Add(material.color);
        }

        currentAlpha = 1;
    }
    private void GetFadingVariables(in GameObject gameObject, out float currentAlpha, out Material material, out Color color)
    {
        material = gameObject.GetComponent<Renderer>().material; // Passing in the Material or Renderer as parameter would be more efficient.
        color = material.color;

        currentAlpha = 1;
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

        return currentAlpha;
    }
    private float UpdateCurrentAlphaBy(float currentAlpha, float alphaDelta, in Material material, Color color)
    {
        currentAlpha += alphaDelta;

        Color col = color;
        col.a = currentAlpha;
        material.color = col;

        return currentAlpha;
    }

    private IEnumerator FadeOutObject(GameObject[] gameObjects)
    {
        GetFadingVariables(in gameObjects, out float currentAlpha, out List<Material> materials, out List<Color> colors);

        while (currentAlpha != 0)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaDecrementor, materials, colors);

            yield return fadeUpdateInterval;
        }
    }
    private IEnumerator FadeOutObject(GameObject gameObject)
    {
        GetFadingVariables(in gameObject, out float currentAlpha, out Material material, out Color color);

        if (gameObject.CompareTag(Tag.Torso)) torsoRenderer.material = torsoTransparant;

        while (currentAlpha != 0)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaDecrementor, material, color);

            yield return fadeUpdateInterval;
        }
    }

    private IEnumerator FadeInObject(GameObject[] gameObjects)
    {
        GetFadingVariables(in gameObjects, out float currentAlpha, out List<Material> materials, out List<Color> colors);

        while (currentAlpha != 0)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaIncrementor, materials, colors);

            yield return fadeUpdateInterval;
        }
    }
    private IEnumerator FadeInObject(GameObject gameObject)
    {
        GetFadingVariables(in gameObject, out float currentAlpha, out Material material, out Color color);

        while (currentAlpha != 0)
        {
            currentAlpha = UpdateCurrentAlphaBy(currentAlpha, alphaIncrementor, material, color);

            yield return fadeUpdateInterval;
        }

        if (gameObject.CompareTag(Tag.Torso)) torsoRenderer.material = torsoOpaque;
    }

    private void Awake()
    {
        traySocket = GameObject.FindGameObjectWithTag(Tag.Tray).GetComponent<XRSocketInteractor>();
        torsoRenderer = torso.GetComponent<Renderer>();
        torsoOpaque = torsoRenderer.material;
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