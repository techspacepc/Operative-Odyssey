using Tags;
using UnityEngine;

public class SimpleOrganDissect : MonoBehaviour
{
    [SerializeField] private GameObject cutOrgan;

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(Tag.Scalpel)) return;

        Dissect();
    }

    private void Dissect()
    {
        Instantiate(cutOrgan, transform.position, Quaternion.identity, transform.parent);

        Destroy(gameObject);
    }
}