using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BubbleInteract : MonoBehaviour
{
    public static event Action<string> OnBubbleInteract;

    private new Renderer renderer;

    private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;


    private XRSimpleInteractable interactable;

    private void BubbleInteracted(SelectEnterEventArgs _)
    {
        renderer.material = selectedMaterial;
        OnBubbleInteract(name);
    }
    private void ChangeToDefaultMaterial(string interactedBubble)
    {
        if (interactedBubble == name) return;

        renderer.material = defaultMaterial;
    }

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        renderer = GetComponent<Renderer>();

        defaultMaterial = renderer.material;
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(BubbleInteracted);
        OnBubbleInteract += ChangeToDefaultMaterial;
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(BubbleInteracted);
        OnBubbleInteract -= ChangeToDefaultMaterial;
    }
}
