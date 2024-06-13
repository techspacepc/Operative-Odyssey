using Organs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashOrgan : MonoBehaviour
{
    private XRSocketInteractor lidSocket;
    private readonly Queue<(Renderer, GameObject)> rendererQueue = new();

    private void Dequeue(SelectEnterEventArgs _)
    {
        while (rendererQueue.Count > 0)
        {
            (Renderer renderer, GameObject gameObject) = rendererQueue.Dequeue();

            renderer.enabled = true;
            Destroy(gameObject);
        }
    }

    private void Awake() => lidSocket = GetComponentInChildren<XRSocketInteractor>();

    private void OnEnable() => lidSocket.selectEntered.AddListener(Dequeue);

    private void OnDisable() => lidSocket.selectEntered.RemoveListener(Dequeue);

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out IOrgan organ)) return;

        rendererQueue.Enqueue((organ.FullOrganRenderer, collision.gameObject));
    }
}