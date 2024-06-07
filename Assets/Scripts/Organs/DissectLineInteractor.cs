using UnityEngine;

public class EdgeToBoxColliderGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 colliderLeniency;

    [ContextMenu(nameof(GenerateBoxColliders))]
    private void GenerateBoxColliders()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        Vector2[] edgePoints = GetComponent<EdgeCollider2D>().points;

        for (int i = 0; i < edgePoints.Length - 1; i++)
        {
            Vector3 pointA = transform.TransformPoint(edgePoints[i]);
            Vector3 pointB = transform.TransformPoint(edgePoints[i + 1]);
            Vector3 center = (pointA + pointB) / 2;

            BoxCollider box = new GameObject($"BoxCollider of point {i} and {i + 1}").AddComponent<BoxCollider>();
            box.transform.parent = transform;
            box.transform.position = center;

            Vector3 direction = pointB - pointA;
            float distance = direction.magnitude;
            box.size = new Vector3(distance, colliderLeniency.x, colliderLeniency.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            box.transform.Rotate(0, 0, angle);

            box.isTrigger = true;
        }
    }
}