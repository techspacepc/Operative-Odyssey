using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHighlight : MonoBehaviour
{
    public GameObject organHighlight;
    private XRGrabInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        if (organHighlight != null)
        {
            organHighlight.SetActive(false);
        }

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrabbed);
            interactable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        print(args);
        print(1);
        if (organHighlight != null)
        {
            organHighlight.SetActive(true);
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        print(args);
        print(2);
        if (organHighlight != null)
        {
            organHighlight.SetActive(false);
        }
    }
}
