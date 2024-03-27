using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetButton : MonoBehaviour
{
    public static event Action OnReset;

    private XRSimpleInteractable interactable;

    private Material material;
    private Color trigger_color = new(1, 0, 0), default_color;

    public void OnSelectEnter(SelectEnterEventArgs _)
    {
        material.color = trigger_color;

        OnReset();
    }

    public void OnSelectExit(SelectExitEventArgs _)
    {
        Debug.Log(nameof(OnSelectExit));

        material.color = default_color;
    }

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        material = GetComponent<Renderer>().material;
        default_color = material.color;
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelectEnter);
        interactable.selectExited.RemoveListener(OnSelectExit);
    }
}