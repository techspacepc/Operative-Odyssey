using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganCollider : MonoBehaviour
{
    public GameObject organ;
    public Vector3 freezePosition;
    public Vector3 freezeRotation;

    void OnTriggerEnter(Collider other){
        //reset position
        //organ.transform.localPosition = freezePosition;

        // Call the function from OtherScript

        Debug.Log("ENTER TRIGGER");
    }
    void OnTriggerStay(Collider other){
        Debug.Log("STAY TRIGGER");

        //organ.transform.localPosition = freezePosition;
    }

    void OnTriggerExit(Collider other){
        Debug.Log("EXIT TRIGGER");

        //organ.GetComponent<FreezeOrgan>().UnFreeze();
    }
}
