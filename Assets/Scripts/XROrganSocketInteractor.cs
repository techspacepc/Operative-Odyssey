using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Organs
{
    [RequireComponent(typeof(OrganRecaller))]
    public class XROrganSocketInteractor : XRSocketInteractor, IOrgan
    {
        [field: SerializeField] public OrganType Organ { get; set; }
        public bool IsGrabbed { get; set; } // Will not get used, is just required to be implemented because of the interface, perhaps there should be an IGrabbable Interface?

        private XRGrabInteractable socketedInteractable;
        private IOrgan socketedOrgan;
        private const float time = 0.1f;

        private void GrabGracePeriod() => socketedOrgan.IsGrabbed = false;
        public void OnGrabbed(SelectEnterEventArgs _) => socketedOrgan.IsGrabbed = true;
        public void OnReleased(SelectExitEventArgs args)
        {
            if (args.interactorObject is XROrganSocketInteractor) return;

            CancelInvoke(nameof(GrabGracePeriod));
            Invoke(nameof(GrabGracePeriod), time);
        }

        protected override void Awake()
        {
            base.Awake();

            socketedOrgan = startingSelectedInteractable.gameObject.AddComponent<OrganIdentifier>().GetComponent<IOrgan>();
            socketedOrgan.Organ = Organ;

            socketedInteractable = startingSelectedInteractable.GetComponent<XRGrabInteractable>();
        }

        private bool MatchOrgan(IXRInteractable interactable)
            => interactable.transform.TryGetComponent(out IOrgan I) && I.Organ == Organ;

        public override bool CanHover(IXRHoverInteractable interactable)
            => MatchOrgan(interactable) && base.CanHover(interactable);

        public override bool CanSelect(IXRSelectInteractable interactable)
            => MatchOrgan(interactable) && base.CanSelect(interactable);

        protected override void OnEnable()
        {
            base.OnEnable();

            socketedInteractable.selectEntered.AddListener(OnGrabbed);
            socketedInteractable.selectExited.AddListener(OnReleased);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            socketedInteractable.selectEntered.RemoveListener(OnGrabbed);
            socketedInteractable.selectExited.RemoveListener(OnReleased);
        }
    }
}