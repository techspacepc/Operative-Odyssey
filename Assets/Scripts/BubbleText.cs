using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private MeshRenderer bubbleRenderer;
    [SerializeField] private Material bubbleMaterial;
    [SerializeField] private Material hoverMaterial;

    void OnTriggerEnter(Collider other){
        text.SetActive(true);
        bubbleRenderer.material = hoverMaterial;
        bubbleRenderer.enabled = false;
    }

    void OnTriggerStay(Collider other){

    }

    void OnTriggerExit(Collider other){
        text.SetActive(false);
        bubbleRenderer.material = bubbleMaterial;
        bubbleRenderer.enabled = true;
    }
}
