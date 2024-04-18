using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Organs
{
    /// <summary>
    /// This script will only be implemented by <code>[RequireComponent(typeof(OrganRecall))]</code> in the <see cref="XROrganSocketInteractor"/> class.
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

            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
                if (script is IOrgan)
                    return;

            throw new InvalidOperationException($"No {nameof(MonoBehaviour)} on this {nameof(GameObject)} implements the {nameof(IOrgan)} interface." +
                $" This script ({nameof(OrganRecaller)}) must only be on {nameof(GameObject)}s that implement the {nameof(IOrgan)} interface");
        }
    }
}