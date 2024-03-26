using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganSnappingPhysics : MonoBehaviour
{
    private Transform child;
    private new Rigidbody rigidbody;

    [SerializeField] private XRGrabInteractable interactable;

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        Debug.Log("Callback");   
    }

    private void Start()
    {
        //interactable = GetComponent<XRGrabInteractable>();

        interactable.selectEntered.AddListener(OnSelectEnter);
        //child = transform.GetChild(0);
        //rigidbody.isKinematic = true;
    }

    /*    private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform == child)
            {
                child.position = transform.position;
                //rigidbody.isKinematic = true;
            }
        }*/
}