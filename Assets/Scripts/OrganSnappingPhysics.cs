using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Yes this class does not do anything right now, turns out the things I needed were presets in the XR environment that I can just use.
/// However this might be used in the future for freezing objects in space - if I learn how to get specific button presses into an if statement.
/// For now this class just serves as an example on how to run events with interactables. (Hence I'm not removing it).
/// </summary>
public class OrganSnappingPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable interactable;

    public void OnSelectEnter(SelectEnterEventArgs _)
    {
        Debug.Log(nameof(OnSelectEnter));
    }

    public void OnSelectExit(SelectExitEventArgs _)
    {
        Debug.Log(nameof(OnSelectExit));

        //rb.isKinematic = false;
    }

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }
}