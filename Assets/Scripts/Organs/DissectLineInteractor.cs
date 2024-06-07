using UnityEngine;

public class LineInteractor : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;

    [ContextMenu(nameof(Start))]
    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();

        Vector2[] edgePoints = edgeCollider.points;

        for (int i = 0; i < edgePoints.Length - 1; i++)
        {
            Vector3 pointA = transform.position + (Vector3)edgePoints[i];
            Vector3 pointB = transform.position + (Vector3)edgePoints[i + 1];
            Vector3 center = (pointA + pointB) / 2;

            BoxCollider box = new GameObject("BoxCollider").AddComponent<BoxCollider>();
            box.transform.parent = this.transform;
            box.transform.localScale = Vector3.one;
            box.transform.localPosition = center;


            Vector3 direction = pointB - pointA;
            float distance = direction.magnitude;
            box.size = new Vector3(distance, 0.1f, 0.1f);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            box.transform.Rotate(0, 0, angle);
        }
    }

    [ContextMenu(nameof(DestroyAllChildren))]
    private void DestroyAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }
}
