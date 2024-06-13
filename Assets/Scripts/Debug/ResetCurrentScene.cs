using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetCurrentScene : MonoBehaviour
{
    private ResetKey resetKey;

    public void ResetScene(InputAction.CallbackContext _) => SceneManager.LoadScene(0);

    private void Awake() => resetKey = new ResetKey();

    private void OnEnable()
    {
        resetKey.Reset.Key.started += ResetScene;
        resetKey.Enable();
    }

    private void OnDisable()
    {
        resetKey.Disable();
        resetKey.Reset.Key.started -= ResetScene;
    }
}