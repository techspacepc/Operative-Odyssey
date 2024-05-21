using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnOnGravitySkeleton : MonoBehaviour
{
    private Rigidbody rigidBody;
    private XRGrabInteractable interactable;

    private void TurnOffKinematic()
    {
        rigidBody.isKinematic = false;
        print("rb kinematic set to false");
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();
    }

    public void TurnOnGravity(SelectExitEventArgs _)
    {
        Invoke(nameof(TurnOffKinematic), 0.01f);
    }

    private void OnEnable()
    {
        interactable.selectExited.AddListener(TurnOnGravity);
    }

    private void OnDisable()
    {
        interactable.selectExited.RemoveListener(TurnOnGravity);
    }

}
