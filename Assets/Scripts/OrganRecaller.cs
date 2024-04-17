using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Organs
{
    /// <summary>
    /// This script will only be implemented by <code>[RequireComponent(typeof(OrganRecall))]</code> in the <see cref="XROrganSocketInteractor"/> class.
    /// </summary>
    public class OrganRecaller : MonoBehaviour
    {
        private readonly XRSocketInteractor socket;

        private Coroutine recallCountdown;
        private readonly WaitForSeconds countdown = new(3);

        private void Recall()
            => socket.interactionManager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

        private IEnumerator RecallCoroutine()
        {
            yield return countdown;

            Recall();
        }

        private void Awake()
        {
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
                if (script is IOrgan organ)
                    return;

            throw new InvalidOperationException($"No {nameof(MonoBehaviour)} on this {nameof(GameObject)} implements the {nameof(IOrgan)} interface." +
                $" This script ({nameof(OrganRecaller)}) must only be on {nameof(GameObject)}s that implement the {nameof(IOrgan)} interface");
        }

        private void OnTriggerExit(Collider other)
        {
            // TODO: Implement bounding box check for when organ exits the playing field.
        }

        private void OnBecameVisible()
        {
            if (recallCountdown != null) return;

            recallCountdown = StartCoroutine(RecallCoroutine());
        }

        private void OnBecameInvisible()
        {
            if (recallCountdown == null) return;

            StopCoroutine(recallCountdown);
            recallCountdown = null;
        }
    }
}