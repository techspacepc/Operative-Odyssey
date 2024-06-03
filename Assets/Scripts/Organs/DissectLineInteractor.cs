using Tags;
using UnityEngine;

public class LineInteractor : MonoBehaviour
{
    private Collider2D line;
    private GameObject scalpel;

    private readonly float tolerance = 0.001f;

    private void Awake()
    {
        line = GetComponent<Collider2D>();
        scalpel = GameObject.FindGameObjectWithTag(Tag.Scalpel);
    }

    private void Update()
    {
        Vector2 point = new(scalpel.transform.position.x, scalpel.transform.position.y);
        Vector2 closestPoint = line.ClosestPoint(point);

        float distance = Vector2.Distance(closestPoint, point);
        if (distance < tolerance)
        {
            Debug.Log($"Logged collision on scalpel point: {point}, relative to closest line point: {closestPoint} with a distance of {distance} to eachother");
        }
    }
}