using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Organs
{
    /// <summary>
    /// This script will only be implemented by <code>[RequireComponent(typeof(OrganRecall))]</code> in the <see cref="XROrganSocketInteractor"/> class.
    /// Though it is also hard-added onto the Scalpel. Technical debt.
    /// </summary>
    public class OrganRecaller : MonoBehaviour
    {
        private XRSocketInteractor socket;

        private void Recall()
            => socket.interactionManager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

        private void Awake()
        {
            socket = GetComponent<XRSocketInteractor>();
            OutOfBoundsChecker checker = socket.startingSelectedInteractable.gameObject.AddComponent<OutOfBoundsChecker>();
            checker.recall = Recall;
        }
    }
}