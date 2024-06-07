using Tags;
using UnityEngine;

public class Incision : MonoBehaviour
{
    private int childIndex;
    private LineRenderer lineRenderer;

    private void Reset()
    {
        lineRenderer.material = IncisionManager.uncutMat;
    }

    private void Awake()
    {
        childIndex = transform.GetSiblingIndex();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tag.Scalpel)) return;

        IncisionManager.onIncisionMade(childIndex);
        lineRenderer.material = IncisionManager.cutMat;
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