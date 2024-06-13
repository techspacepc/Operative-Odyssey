using UnityEngine;

public class CheckTriggerEnter : MonoBehaviour
{

    public delegate void checkTriggerEnterDelegate(GameObject obj);
    public event checkTriggerEnterDelegate checkTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkTriggerEnterEvent(gameObject);
        }
    }
}
