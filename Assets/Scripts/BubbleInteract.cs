using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class BubbleInteract : MonoBehaviour
{
    //public static event Action<string> OnBubbleInteract;
    private GameObject[] allBubbles;
    private new Renderer renderer;

    private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;
    
    private GameObject textObject;

    private XRSimpleInteractable interactable;

    private void BubbleInteracted(SelectEnterEventArgs _)
    {
        //iterate through all bubbles to check whether they are activated, set unactive if so
        for (int i = 0; i < allBubbles.Length; i++)
        {
            Transform textTransform = allBubbles[i].transform.GetChild(0);
            GameObject text = textTransform.gameObject;

            //check if the text is active
            if(text.activeSelf){
                //check if the bubble is the interactedbubble
                if(allBubbles[i].name != name){
                    //disable the text
                    text.SetActive(false);

                    //change to default material
                    allBubbles[i].GetComponent<Renderer>().material = defaultMaterial;
                }
            }
        }

        if (textObject.activeSelf){
            //deselect the bubble
            renderer.material = defaultMaterial;
            //OnBubbleInteract(name);
            textObject.SetActive(false);
        }else{
            //select the bubble
            renderer.material = selectedMaterial;
            //OnBubbleInteract(name);
            textObject.SetActive(true);
        }
    }

    /*
    private void ChangeToDefaultMaterial(string interactedBubble)
    {
        if (interactedBubble == name) return;
        renderer.material = defaultMaterial;
    }
    */

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        renderer = GetComponent<Renderer>();

        defaultMaterial = renderer.material;

        // Get the child textobject by index
        textObject = transform.GetChild(0).gameObject;

        //set name text
        TextMeshPro text = textObject.GetComponent<TextMeshPro>();
        text.text = name;

        //set position of the text on the bubble
        RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
        textRectTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Start(){
        //get all bubbles
        allBubbles = BubbleHandler.bubbleArray;
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(BubbleInteracted);
        //OnBubbleInteract += ChangeToDefaultMaterial;
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(BubbleInteracted);
        //OnBubbleInteract -= ChangeToDefaultMaterial;
    }
}
