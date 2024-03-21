using UnityEngine;

public class OrganSnappingPhysics : MonoBehaviour
{
    private Transform child;
    private new Rigidbody rigidbody;

    private void Start()
    {
        child = transform.GetChild(0);
        rigidbody = child.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == child)
        {
            child.position = transform.position;
            rigidbody.isKinematic = true;
        }
    }
}