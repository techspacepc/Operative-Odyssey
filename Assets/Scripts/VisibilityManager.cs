using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VisibilityManager : MonoBehaviour
{
    [SerializeField] public GameObject Tray;
    [SerializeField] public GameObject Torso;
    [SerializeField] public GameObject Knife;

    private XRSocketInteractor socket;

    private void Awake()
    {
        socket = Tray.GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnObjectInserted);
        socket.selectExited.AddListener(OnObjectRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnObjectInserted);
        socket.selectExited.RemoveListener(OnObjectRemoved);
    }

    private void Start()
    {
        Knife.SetActive(false);
    }

    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        Torso.SetActive(false);
        Knife.SetActive(true);
    }

    private void OnObjectRemoved(SelectExitEventArgs args)
    {
        Torso.SetActive(true);
        Knife.SetActive(false);
    }
}
