using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject objectToDisable;

    void Start()
    {
        // Disable the object when the script starts
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No object assigned to be disabled.");
        }
    }

    // You can also create a method to disable the object whenever needed
    public void DisableObj()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No object assigned to be disabled.");
        }
    }

    public void EnableObj()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No object assigned to be disabled.");
        }
    }
}
