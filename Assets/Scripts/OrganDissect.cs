using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganDissect : MonoBehaviour
{
    private bool dissecting;
    private Vector3 enterPoint, exitPoint;
    [SerializeField] private GameObject half;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scalpel"))
        {
            dissecting = true;
            enterPoint = transform.position - other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Scalpel") && dissecting)
        {
            dissect();
            exitPoint = transform.position - other.transform.position;
        }
    }

    private void dissect()
    {
        GameObject leftHalf = Instantiate(half, transform.position, transform.rotation);
        GameObject rightHalf = Instantiate(half, transform.position, transform.rotation);

        rightHalf.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);

        Destroy(gameObject);
    }
}
