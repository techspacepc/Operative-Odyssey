using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabLogic : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Transform primaryAttachTransform;
    [SerializeField] private Transform secondaryAttachTransform;

    void Start()
    {
        // Ensure XRGrabInteractable component is assigned
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }
    }

    public void SetAttachTransformLeftHand()
    {
        grabInteractable.attachTransform = primaryAttachTransform;
    }

    public void SetAttachTransformRightHand()
    {
        grabInteractable.attachTransform = secondaryAttachTransform;
    }
}
