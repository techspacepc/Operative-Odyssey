using Organs;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// I am pretty sure I way overcomplicated this, I think I can literally just get the startingSelectedInteractable instance and set the functions to true if it's that instance else false. 
/// I am going to cry.
/// </summary>
public class XROrganSocketInteractor : XRSocketInteractor, IOrgan
{
    [field: SerializeField] public OrganType Organ { get; set; }

    protected override void Awake()
    {
        base.Awake();

        startingSelectedInteractable.gameObject.AddComponent<OrganIdentifier>().Organ = Organ;
    }

    private bool MatchOrgan(IXRInteractable interactable)
        => interactable.transform.TryGetComponent(out IOrgan I) && I.Organ == Organ;

    public override bool CanHover(IXRHoverInteractable interactable)
        => MatchOrgan(interactable) && base.CanHover(interactable);

    public override bool CanSelect(IXRSelectInteractable interactable)
        => MatchOrgan(interactable) && base.CanSelect(interactable);
}