using System;
using System.Collections;
using UnityEngine;

namespace Organs
{
    public class OutOfBoundsChecker : MonoBehaviour
    {
        /// <summary> Assigned by <see cref="OrganRecaller"/> </summary>
        public Action recall;

        private Coroutine recallCountdown;
        private readonly WaitForSeconds countdown = new(3);

        public void RecallThis(GameObject gameObject)
        {
            if (gameObject == this.gameObject)
                recall();
        }

        private IEnumerator RecallCoroutine()
        {
            yield return countdown;
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

        private void OnEnable() => BoundingDelegater.OnOrganExit += RecallThis;
        private void OnDisable() => BoundingDelegater.OnOrganExit -= RecallThis;
    }
}
