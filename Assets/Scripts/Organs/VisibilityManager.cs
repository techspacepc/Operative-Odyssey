using Constants;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VisibilityManager : MonoBehaviour
{
    [SerializeField] GameObject torso, scalpel;

    [SerializeField] private Material torsoTransparant;
    private Material torsoCurrent, torsoOpaque;

    private XRSocketInteractor traySocket;

    private void OnOrganSocketed(SelectEnterEventArgs args)
    {
        if (traySocket.interactablesSelected.Count == 0) return;

        StartCoroutine(FadeOutObject(null));
    }

    private void OnOrganUnsocketed(SelectExitEventArgs args)
    {
        if (traySocket.interactablesSelected.Count > 0) return;

        StartCoroutine(FadeInObject());
    }

    private IEnumerator FadeOutObject(GameObject[] gameObjects)
    {
        yield return null;
    }

    private IEnumerator FadeInObject()
    {
        yield return null;
    }

    private void Awake()
    {
        traySocket = GameObject.FindGameObjectWithTag(Tag.Tray).GetComponent<XRSocketInteractor>();
        torsoOpaque = torsoCurrent = torso.GetComponent<Renderer>().material;
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