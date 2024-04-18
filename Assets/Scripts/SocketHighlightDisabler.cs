using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHighlightDisabler : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    private BoxCollider boxCollider;

    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        boxCollider = GetComponent<BoxCollider>();

        socketInteractor.socketSnappingRadius = 0.001f;
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        boxCollider.enabled = false;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        boxCollider.enabled = true;
    }
}
