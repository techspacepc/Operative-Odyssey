using Tags;
using UnityEngine;

public class Incision : MonoBehaviour
{
    private int childIndex;
    private new Renderer renderer;

    private void Reset()
    {
        renderer.material = IncisionManager.uncutMat;
    }

    private void Awake()
    {
        childIndex = transform.GetSiblingIndex();
        renderer = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tag.Scalpel)) return;

        IncisionManager.onIncisionMade?.Invoke(childIndex);
        renderer.material = IncisionManager.cutMat;
    }

    private void OnEnable()
    {
        IncisionManager.OnIncisionFailed += Reset;
    }

    private void OnDisable()
    {
        IncisionManager.OnIncisionFailed -= Reset;
    }
}