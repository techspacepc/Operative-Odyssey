using Organs;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROrganSocketInteractor : XRSocketInteractor, IOrgan
{
    [field: SerializeField] public OrganType Organ { get; set; }

    protected override void Awake()
    {
        base.Awake();

        startingSelectedInteractable.gameObject.AddComponent<OrganIdentifier>().Organ = Organ;
        startingSelectedInteractable.gameObject.AddComponent<SocketHighlight>();
    }

    private bool MatchOrgan(IXRInteractable interactable)
        => interactable.transform.TryGetComponent(out IOrgan I) && I.Organ == Organ;

    public override bool CanHover(IXRHoverInteractable interactable)
        => MatchOrgan(interactable) && base.CanHover(interactable);

    public override bool CanSelect(IXRSelectInteractable interactable)
        => MatchOrgan(interactable) && base.CanSelect(interactable);
}