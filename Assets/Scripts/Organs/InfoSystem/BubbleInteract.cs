using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BubbleInteract : MonoBehaviour
{
    public static event Action<string> OnBubbleInteract;
    private GameObject[] allBubbles;
    private new Renderer renderer;

    private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;

    [SerializeField] private GameObject textObject;  // Serialized field for text object
    [SerializeField] private GameObject textMeshPro;  // Serialized field for TextMeshPro component

    private XRSimpleInteractable interactable;

    private void BubbleInteracted(SelectEnterEventArgs _)
    {
        // Iterate through all bubbles to check whether they are activated, set unactive if so
        for (int i = 0; i < allBubbles.Length; i++)
        {
            BubbleInteract bubbleInteract = allBubbles[i].GetComponent<BubbleInteract>();
            GameObject text = bubbleInteract.textObject;

            // Check if the text is active
            if (text.activeSelf)
            {
                // Check if the bubble is the interacted bubble
                if (allBubbles[i].name != name)
                {
                    // Disable the text
                    text.SetActive(false);

                    // Change to default material
                    allBubbles[i].GetComponent<Renderer>().material = defaultMaterial;
                }
            }
        }

        if (textObject.activeSelf)
        {
            // Deselect the bubble
            renderer.material = defaultMaterial;
            // OnBubbleInteract(name);
            textObject.SetActive(false);
        }
        else
        {
            // Select the bubble
            renderer.material = selectedMaterial;
            OnBubbleInteract?.Invoke(name);
            textObject.SetActive(true);
        }
    }

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        renderer = GetComponent<Renderer>();

        defaultMaterial = renderer.material;

        // Check if textObject and textMeshPro are assigned in the Inspector
        if (textObject == null)
        {
            Debug.LogError("Text Object is not assigned in the inspector for " + name);
            return;  // Exit early to avoid null reference exceptions
        }

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned in the inspector for " + name);
            return;  // Exit early to avoid null reference exceptions
        }

        // Set name text
        TextMeshProUGUI TextMeshProText = textMeshPro.GetComponent<TextMeshProUGUI>();
        TextMeshProText.text = name;

        // Set position of the text on the bubble
        RectTransform textRectTransform = textMeshPro.GetComponent<RectTransform>();
        textRectTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Start()
    {
        // Get all bubbles
        allBubbles = BubbleHandler.bubbleArray;
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(BubbleInteracted);
        // OnBubbleInteract += ChangeToDefaultMaterial;
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(BubbleInteracted);
        // OnBubbleInteract -= ChangeToDefaultMaterial;
    }
}
