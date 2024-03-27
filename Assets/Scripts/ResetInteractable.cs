using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetInteractable : MonoBehaviour
{
    private XRSocketInteractor socket;

    private void ResetObject()
        => socket.interactionManager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

    private void Awake() => socket = GetComponent<XRSocketInteractor>();

    private void OnEnable() => ResetButton.OnReset += ResetObject;

    private void OnDisable() => ResetButton.OnReset -= ResetObject;
}