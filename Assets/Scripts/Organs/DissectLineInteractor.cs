using Tags;
using UnityEngine;

public class LineInteractor : MonoBehaviour
{
    private EdgeCollider2D line;
    private GameObject scalpel;
    private GameObject boxColliders;

    private Vector2 direction;
    private Vector3 defaultSize = Vector3.one / 10;

    private void Awake()
    {
        line = GetComponent<EdgeCollider2D>();
        scalpel = GameObject.FindGameObjectWithTag(Tag.Scalpel);
        boxColliders = transform.GetChild(0).gameObject;

        CreateBoxCollidersOnEdges();
    }

    [ContextMenu(nameof(CreateBoxCollidersOnEdges))]
    private void CreateBoxCollidersOnEdges() 
    {
        line = GetComponent<EdgeCollider2D>();
        scalpel = GameObject.FindGameObjectWithTag(Tag.Scalpel);
        boxColliders = transform.GetChild(0).gameObject;

        for (int i = 0; i < line.points.Length - 2; i++)
        {
            Vector3 pointA = (Vector3)line.points[i];
            Vector3 pointB = (Vector3)line.points[i + 1];
            direction = pointB - pointA;


            BoxCollider box = new GameObject("BoxCollider").AddComponent<BoxCollider>();
            box.transform.parent = boxColliders.transform;
            Vector3 center = CalculateCenterBetweenPoints(pointA, pointB);
            box.transform.position = center;
            box.center = center;
            //box.size = CalculateSizeBetweenPoints(pointA, pointB);
            box.size = defaultSize;
            //box.transform.rotation = CalculateRotation(direction);
        }
    }
    private Vector3 CalculateCenterBetweenPoints(Vector2 a, Vector2 b)
    {
        Vector2 midpoint = (a + b) / 2;

        Vector3 center = new(midpoint.x, midpoint.y, 0);

        return center;
    }

    public Vector3 CalculateSizeBetweenPoints(Vector2 a, Vector2 b)
    {
        float distance = Vector2.Distance(a, b);

        Vector2 normalizedDirection = direction.normalized;

        Vector3 size = new(
            Mathf.Abs(normalizedDirection.x) * distance + defaultSize.x * (1 - Mathf.Abs(normalizedDirection.x)),
            Mathf.Abs(normalizedDirection.y) * distance + defaultSize.y * (1 - Mathf.Abs(normalizedDirection.y)),
            defaultSize.z
        );

        return size;
    }

    private Quaternion CalculateRotation(Vector2 direction)
    {
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);

        Quaternion rotation = Quaternion.Euler(0, 0, angleInRadians * Mathf.Rad2Deg);

        return rotation;
    }
}