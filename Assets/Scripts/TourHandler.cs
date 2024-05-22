using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourHandler : MonoBehaviour
{
    [SerializeField] private GameObject heartTour;
    [SerializeField] private GameObject kidneyTour;
    [SerializeField] private GameObject eyeTour;
    [SerializeField] private GameObject museum;
    [SerializeField] private GameObject player;
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
            heartTour.SetActive(true);
            museum.SetActive(false);

            colliderGate.enabled = false;
        }

        Debug.Log("ENTERING");
    }

    public void EndTour()
    {
        heartTour.SetActive(false);
        kidneyTour.SetActive(false);
        eyeTour.SetActive(false);
        museum.SetActive(true);

        player.transform.position = newPosition;

        Invoke(nameof(EnableCollider), 0.1f);
    }

    private void EnableCollider(){
        colliderGate.enabled = true;
    }
}
