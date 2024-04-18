using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private const float offset = 0.5f;

    private void Update()
    {
        Vector3 direction = player.position - transform.position;

        direction.y = direction.x + offset;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}