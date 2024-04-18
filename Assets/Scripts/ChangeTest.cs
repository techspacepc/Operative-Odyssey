using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTest : MonoBehaviour
{
    [SerializeField] private GameObject test1;
    [SerializeField] private GameObject test2;
    
    [SerializeField] private GameObject test3;

    public void ChangeBubbleTest(){
        // Get the child by index
        TextMeshPro textMeshPro = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        
        if (test1.activeSelf){
            test1.SetActive(false);
            test2.SetActive(true);
            textMeshPro.text = "Test 2: \n Bubble hover interaction";
        }else if (test2.activeSelf){
            test2.SetActive(false);
            test3.SetActive(true);
            textMeshPro.text = "Test 3: \n Bubble direct interaction";
        }else{
            test3.SetActive(false);
            test1.SetActive(true);
            textMeshPro.text = "Test 1: \n Bubble ray interaction";
        }
        
    }
}
