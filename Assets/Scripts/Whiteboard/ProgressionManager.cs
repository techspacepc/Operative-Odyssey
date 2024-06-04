using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ProgressionManager : MonoBehaviour
{
    private void TheBubbleInteract(string bubbleName)
    {
        print(bubbleName);
    }

    private void OnEnable()
    {
        BubbleInteract.OnBubbleInteract += TheBubbleInteract;
    }

    private void OnDisable()
    {
        BubbleInteract.OnBubbleInteract -= TheBubbleInteract;
    }
}