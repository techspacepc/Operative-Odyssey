using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewTour : MonoBehaviour
{
    [SerializeField] private GameObject oldTour;
    [SerializeField] private GameObject newTour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oldTour.SetActive(false);
            newTour.SetActive(true);
        }
    }
}
