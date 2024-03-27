using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetInteractable : MonoBehaviour
{
    private XRSocketInteractor socket;

    private XRInteractionManager manager;

    private void ResetObject()
        => manager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();

        // This should really be a singleton in scene, so it can be statically accessed.
        manager = FindObjectOfType<XRInteractionManager>();
    }

    private void OnEnable() => ResetButton.OnReset += ResetObject;

    private void OnDisable() => ResetButton.OnReset -= ResetObject;
}