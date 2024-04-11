using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Organs
{
    /// <summary>
    /// This script will only be implemented by <code>[RequireComponent(typeof(OrganRecall))]</code> in the <see cref="XROrganSocketInteractor"/> class.
    /// </summary>
    public class OrganRecall : MonoBehaviour
    {
        private XRSocketInteractor socket;

        private void Recall()
            => socket.interactionManager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

        private void Awake() => socket = GetComponent<XRSocketInteractor>();
    }
}