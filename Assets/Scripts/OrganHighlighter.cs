using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganHighlighter : MonoBehaviour
{
    private XRSocketInteractor socket;

    private void UpdateSocketHoverMesh(Material material)
    {
        socket.interactableHoverMeshMaterial = material;
    }

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        SocketHighlight.OnOrganGrabbed += UpdateSocketHoverMesh;
    }

    private void OnDisable()
    {
        SocketHighlight.OnOrganGrabbed -= UpdateSocketHoverMesh;
    }
}
