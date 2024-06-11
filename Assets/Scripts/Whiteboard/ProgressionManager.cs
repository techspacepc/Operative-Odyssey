using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ProgressionManager : MonoBehaviour
{
    public ParticleSystem confettiParticleSystem;

    private HashSet<string> interactedBubbles = new();
    private int totalBubbles;

    private void Start()
    {
        totalBubbles = BubbleHandler.bubbleArray.Length;
    }

    private void TheBubbleInteract(string bubbleName)
    {
        if (interactedBubbles.Contains(bubbleName))
        {
            return;
        }

        interactedBubbles.Add(bubbleName);

        if (interactedBubbles.Count >= totalBubbles)
        {
            confettiParticleSystem.Play();
        }
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
