using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHeartTour : MonoBehaviour
{
    [SerializeField] private GameObject tour;
    [SerializeField] private GameObject museum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tour.SetActive(true);
            museum.SetActive(false);
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
    }
}
