using UnityEngine;

public class OrganSnappingCollider : MonoBehaviour
{
    void Start()
    {
        bool isNewCollider = false;

        if (!gameObject.TryGetComponent(out SphereCollider sphere))
        {
            sphere = gameObject.AddComponent<SphereCollider>();
            isNewCollider = true;
        }

        SphereCollider snappingPoint = new GameObject(
            "SnappingPoint",
            typeof(SphereCollider),
            typeof(Rigidbody),
            typeof(OrganSnappingPhysics)).GetComponent<SphereCollider>();

        snappingPoint.radius = sphere.radius;
        snappingPoint.center = sphere.center;

        if (isNewCollider)
            Destroy(sphere);

        gameObject.transform.SetParent(snappingPoint.transform);
    }
}