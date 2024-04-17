using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganDissect : MonoBehaviour
{
    private bool dissecting;
    private bool grabbed;
    private Vector3 enterPoint, exitPoint;

    [Header("Symmetrical Dissection")]
    [SerializeField] private bool cutSymmetrical;
    [SerializeField] private GameObject half;

    [System.Serializable]
    public class Data
    {
        public GameObject obj;
        public Vector3 loc;
    }

    [Header("Asymmetrical Dissection")]
    [SerializeField] public Data[] parts;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scalpel") && !grabbed)
        {
            dissecting = true;
            enterPoint = transform.position - other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Scalpel") && dissecting && !grabbed)
        {
            Dissect();
            exitPoint = transform.position - other.transform.position;
        }
    }

    private void Dissect()
    {
        if (cutSymmetrical)
        {
            GameObject leftHalf = Instantiate(half, transform.position, transform.rotation);
            GameObject rightHalf = Instantiate(half, transform.position, transform.rotation);

            rightHalf.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
        }
        else
        {
            for (int i = 0; i < parts.Length; i++)
            {
                Instantiate(parts[i].obj, transform.position + parts[i].loc, transform.rotation);
            }
        }

        Destroy(gameObject);
    }

    public void Grabbed()
    {
        dissecting = false;
        grabbed = true;
    }

    public void Released()
    {
        grabbed = false;
    }
}
