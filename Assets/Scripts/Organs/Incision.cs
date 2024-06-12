using Tags;
using UnityEngine;

public class Incision : MonoBehaviour
{
    private int childIndex;
    private new Renderer renderer;
    private IncisionManager incisionManager;

    private void Reset()
    {
        renderer.material = IncisionManager.uncutMat;
    }

    private void Awake()
    {
        childIndex = transform.GetSiblingIndex();
        renderer = GetComponentInChildren<Renderer>();
        incisionManager = GetComponentInParent<IncisionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tag.Scalpel)) return;

        incisionManager.onIncisionMade?.Invoke(childIndex);
        renderer.material = IncisionManager.cutMat;
    }

    private void OnEnable()
    {
        incisionManager.ResetIncisions += Reset;
    }

    private void OnDisable()
    {
        incisionManager.ResetIncisions -= Reset;
    }
}