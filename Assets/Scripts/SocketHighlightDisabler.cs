using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHighlightDisabler : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    private BoxCollider boxCollider;

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        boxCollider.enabled = false;
        Invoke(nameof(DelayBoxColliderReEnable), 0.2f);
        Invoke(nameof(DelaySocketDisable), 0.1f);
    }

    private void DelayBoxColliderReEnable() => boxCollider.enabled = true;
    private void DelaySocketDisable() => socketInteractor.enabled = false;

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        boxCollider = GetComponent<BoxCollider>();

        socketInteractor.socketSnappingRadius = 0.001f;
    }

    private void OnEnable() => socketInteractor.selectEntered.AddListener(OnSelectEntered);

    private void OnTriggerExit(Collider other) => socketInteractor.enabled = true;

    private void OnDisable() => socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
}