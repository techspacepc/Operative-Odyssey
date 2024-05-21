using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTour : MonoBehaviour
{
    [SerializeField] private TourHandler tourHandler;

    // Start is called before the first frame update
    public void EndThisTour()
    {
        tourHandler.EndTour();
    }
}
