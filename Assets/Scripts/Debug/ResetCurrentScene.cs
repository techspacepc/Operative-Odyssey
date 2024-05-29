using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetCurrentScene : MonoBehaviour
{
    private XRSimpleInteractable interactable;

    public void OnSelectEnter(SelectEnterEventArgs _) => SceneManager.LoadScene(0);

    private void Awake() => interactable = GetComponent<XRSimpleInteractable>();

    private void OnEnable() => interactable.selectEntered.AddListener(OnSelectEnter);
    private void OnDisable() => interactable.selectEntered.RemoveListener(OnSelectEnter);
}