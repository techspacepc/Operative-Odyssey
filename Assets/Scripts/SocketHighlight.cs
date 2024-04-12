using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHighlight : MonoBehaviour
{
    public static event Action<Material> OnOrganGrabbed;

    private XRGrabInteractable interactable;

    private Material material;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs _)
    {
        OnOrganGrabbed?.Invoke(material);
    }
}