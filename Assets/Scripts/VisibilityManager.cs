using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VisibilityManager : MonoBehaviour
{
    [SerializeField] private GameObject tray;
    [SerializeField] private GameObject torso;
    [SerializeField] private GameObject scalpel;
    [SerializeField] private Material transparantMaterial;

    private new Renderer renderer;
    private Renderer torsoRenderer;
    private Material opaqueMaterial;

    private XRSocketInteractor socket;
    private XRGrabInteractable scalpelGrabInteractable;
    private Material scalpelMaterial;
    private bool isScalpelFading = false;
    private const float scalpelFadeDuration = 1.0f;
    private const float scalpelAlpha = 0.25f;
    private float scalpelFadeTimer = 0.0f;

    private void Awake()
    {
        socket = tray.GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnObjectInserted);
        socket.selectExited.AddListener(OnObjectRemoved);

        renderer = torso.GetComponent<Renderer>();
        torsoRenderer = torso.GetComponent<Renderer>();
        opaqueMaterial = renderer.material;
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjectInserted);
        socket.selectExited.RemoveListener(OnObjectRemoved);
    }

    private void Start()
    {
        scalpelGrabInteractable = scalpel.GetComponent<XRGrabInteractable>();
        scalpelGrabInteractable.enabled = false;

        Renderer scalpelRenderer;
        scalpelRenderer = scalpel.GetComponent<Renderer>();
        scalpelMaterial = scalpelRenderer.material;

        Color scalpelColor = scalpelMaterial.color;
        scalpelColor.a = scalpelAlpha;
        scalpelMaterial.color = scalpelColor;
    }

    private void Update()
    {
        if (isScalpelFading)
        {
            float alpha = Mathf.Lerp(scalpelAlpha, 1.0f, scalpelFadeTimer / scalpelFadeDuration);
            Color scalpelColor = scalpelMaterial.color;
            scalpelColor.a = alpha;
            scalpelMaterial.color = scalpelColor;

            scalpelFadeTimer += Time.deltaTime;

            if (scalpelFadeTimer >= scalpelFadeDuration)
            {
                isScalpelFading = false;
            }
        }
    }

    private void ChangeToTransparant()
    {
        renderer.material = transparantMaterial;
    }

    private void ChangeToOpaque()
    {
        renderer.material = opaqueMaterial;
    }

    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        if (socket.interactablesSelected.Count == 0)
        {
            return;
        }
        StartCoroutine(FadeOutTorso());
        scalpelGrabInteractable.enabled = true;

        isScalpelFading = true;
        scalpelFadeTimer = 0.0f;
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        if (socket.interactablesSelected.Count > 0)
        {
            return;
        }
        torso.SetActive(true);
        scalpelGrabInteractable.enabled = false;

        StartCoroutine(FadeOutKnife());
        StartCoroutine(FadeInTorso());

    }

    private IEnumerator FadeOutTorso()
    {
        ChangeToTransparant();
        Color torsoColor = torsoRenderer.material.color;
        torsoColor.a = 1.0f;
        torsoRenderer.material.color = torsoColor;

        float timer = 0.0f;
        while (timer < scalpelFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, timer / scalpelFadeDuration);

            torsoColor.a = alpha;
            torsoRenderer.material.color = torsoColor;

            yield return null;
        }

        ChangeToOpaque();
        torso.SetActive(false);
    }

    private IEnumerator FadeInTorso()
    {
        ChangeToTransparant();
        Color torsoColor = torsoRenderer.material.color;
        torsoColor.a = 0.0f;
        torsoRenderer.material.color = torsoColor;

        float timer = 0.0f;
        while (timer < scalpelFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, timer / scalpelFadeDuration);

            torsoColor.a = alpha;
            torsoRenderer.material.color = torsoColor;

            yield return null;
        }
        ChangeToOpaque();
    }

    private IEnumerator FadeOutKnife()
    {
        float timer = 0.0f;
        while (timer < scalpelFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, scalpelAlpha, timer / scalpelFadeDuration);

            Color scalpelColor = scalpelMaterial.color;
            scalpelColor.a = alpha;
            scalpelMaterial.color = scalpelColor;

            yield return null;
        }
    }
}