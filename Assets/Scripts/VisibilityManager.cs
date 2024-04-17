using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VisibilityManager : MonoBehaviour
{
    [SerializeField] public GameObject Tray;
    [SerializeField] public GameObject Torso;
    [SerializeField] public GameObject Knife;

    private XRSocketInteractor socket;
    private XRGrabInteractable knifeGrabInteractable;
    private Renderer knifeRenderer;
    private Material knifeMaterial;
    private bool isKnifeFading = false;
    private float knifeFadeDuration = 1.0f;
    private float knifeFadeTimer = 0.0f;

    private void Awake()
    {
        socket = Tray.GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnObjectInserted);
        socket.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjectInserted);
        socket.selectExited.RemoveListener(OnObjectRemoved);
    }

    private void Start()
    {
        knifeGrabInteractable = Knife.GetComponent<XRGrabInteractable>();
        knifeGrabInteractable.enabled = false;

        knifeRenderer = Knife.GetComponent<Renderer>();
        knifeMaterial = knifeRenderer.material;

        Color knifeColor = knifeMaterial.color;
        knifeColor.a = 0.0f;
        knifeMaterial.color = knifeColor;
    }

    private void Update()
    {
        if (isKnifeFading)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, knifeFadeTimer / knifeFadeDuration);
            Color knifeColor = knifeMaterial.color;
            knifeColor.a = alpha;
            knifeMaterial.color = knifeColor;

            knifeFadeTimer += Time.deltaTime;

            if (knifeFadeTimer >= knifeFadeDuration)
            {
                isKnifeFading = false;
            }
        }
    }

    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        if (socket.interactablesSelected.Count == 0)
        {
            return;
        }
        StartCoroutine(FadeOutTorso());
        knifeGrabInteractable.enabled = true;

        isKnifeFading = true;
        knifeFadeTimer = 0.0f;
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        if (socket.interactablesSelected.Count > 0)
        {
            return;
        }
        Torso.SetActive(true);
        knifeGrabInteractable.enabled = false;

        StartCoroutine(FadeInKnife());
        StartCoroutine(FadeInTorso());

    }

    private System.Collections.IEnumerator FadeOutTorso()
    {
        Color torsoColor = Torso.GetComponent<Renderer>().material.color;
        torsoColor.a = 1.0f;
        Torso.GetComponent<Renderer>().material.color = torsoColor;

        float timer = 0.0f;
        while (timer < knifeFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, timer / knifeFadeDuration);

            torsoColor.a = alpha;
            Torso.GetComponent<Renderer>().material.color = torsoColor;

            yield return null;
        }

        Torso.SetActive(false);
    }

    private System.Collections.IEnumerator FadeInTorso()
    {
        Color torsoColor = Torso.GetComponent<Renderer>().material.color;
        torsoColor.a = 0.0f;
        Torso.GetComponent<Renderer>().material.color = torsoColor;

        float timer = 0.0f;
        while (timer < knifeFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, timer / knifeFadeDuration);

            torsoColor.a = alpha;
            Torso.GetComponent<Renderer>().material.color = torsoColor;

            yield return null;
        }
    }

    private System.Collections.IEnumerator FadeInKnife()
    {
        float timer = 0.0f;
        while (timer < knifeFadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, timer / knifeFadeDuration);

            Color knifeColor = knifeMaterial.color;
            knifeColor.a = alpha;
            knifeMaterial.color = knifeColor;

            yield return null;
        }
    }
}
