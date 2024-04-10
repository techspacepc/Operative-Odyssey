using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This should be merged into the <see cref="XROrganSocketInteractor"/> class. 
/// But I will save this rework for later, other things have priority right now.
/// </summary>
public class OrganRecall : MonoBehaviour
{
    private XRSocketInteractor socket;

    private void ResetObject()
        => socket.interactionManager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

    private void Awake() => socket = GetComponent<XRSocketInteractor>();

    private void OnEnable() => ResetButton.OnReset += ResetObject;

    private void OnDisable() => ResetButton.OnReset -= ResetObject;
}