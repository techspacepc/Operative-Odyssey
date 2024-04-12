using System.Collections;
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

        private Coroutine recallCountdown;
        private readonly WaitForSeconds countdown = new(3);

        private void Recall()
            => socket.interactionManager.SelectEnter(socket as IXRSelectInteractor, socket.startingSelectedInteractable);

        private IEnumerator RecallCoroutine()
        {
            yield return countdown;

            Recall();
        }

        private void Awake() => socket = GetComponent<XRSocketInteractor>();

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