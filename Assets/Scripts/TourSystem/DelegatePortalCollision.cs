using System;
using Tags;
using UnityEngine;

public class DelegatePortalCollision : MonoBehaviour
{
    public static event Action OnPortalEntered;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tag.Player)) return;

        OnPortalEntered();
    }
}