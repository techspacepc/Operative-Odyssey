using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRay : MonoBehaviour
{
    [SerializeField] private GameObject leftRay;
    [SerializeField] private GameObject rightRay;

    // Update is called once per frame
    void Update()
    {
        leftRay.SetActive(false);
        rightRay.SetActive(false);
    }
}
