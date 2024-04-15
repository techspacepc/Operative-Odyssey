using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabLogic : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Transform primaryAttachTransform;
    [SerializeField] private Transform secondaryAttachTransform;

    public void SetAttachTransformLeftHand()
        => grabInteractable.attachTransform = primaryAttachTransform;

    public void SetAttachTransformRightHand()
        => grabInteractable.attachTransform = secondaryAttachTransform;
}