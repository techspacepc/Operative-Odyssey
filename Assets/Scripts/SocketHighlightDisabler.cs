using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHighlightDisabler : MonoBehaviour
{
    public GameObject organHighlight;
    private XRSocketInteractor socketInteractor;

    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(OnObjectInserted);
    }

    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        organHighlight.SetActive(false);
    }
}
