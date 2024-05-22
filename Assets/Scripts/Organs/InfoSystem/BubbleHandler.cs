using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHandler : MonoBehaviour
{
    private List<GameObject> bubbleList = new List<GameObject>();
    public GameObject[] bubbleArray;

    private void Awake()
    {
        // Iterate through all child objects to get and save all bubbles
        for (int i = 0; i < transform.childCount; i++)
        {
            // Get the i-th child object
            Transform childTransform = transform.GetChild(i);

            // Get the GameObject component of the child Transform
            GameObject childObject = childTransform.gameObject;

            // Add the child object to the list

            if(childObject.tag == "Bubble"){
                bubbleList.Add(childObject);
                //to array
                bubbleArray = bubbleList.ToArray();
            }
        }
    }
}
