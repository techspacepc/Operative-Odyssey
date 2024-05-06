using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHeartTour : MonoBehaviour
{
    [SerializeField] private GameObject tour;
    [SerializeField] private GameObject museum;
    [SerializeField] private GameObject player;
    private const float speed = 5f; // Speed at which the object moves
    private Collider colliderGate;
    private Vector3 newPosition = new Vector3(0, 0, 0);

    private void Start(){
        // Check if the GameObject has a Collider component
        colliderGate = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tour.SetActive(true);
            museum.SetActive(false);

            colliderGate.enabled = false;

            //Debug.Log("COLLIDING");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }

    public void EndTour()
    {
        tour.SetActive(false);
        museum.SetActive(true);

        //Debug.Log("BACK TO MUSUEM");
        player.transform.position = newPosition;

        colliderGate.enabled = true;
    }
}
