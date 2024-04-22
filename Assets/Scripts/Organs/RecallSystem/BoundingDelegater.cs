using Organs;
using System;
using UnityEngine;

public class BoundingDelegater : MonoBehaviour
{
    public static event Action<GameObject> OnOrganExit;

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out IOrgan organ) || organ.IsGrabbed) return;

        OnOrganExit(other.gameObject);
    }
}