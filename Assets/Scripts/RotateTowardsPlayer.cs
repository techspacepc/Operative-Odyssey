using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    // Reference to the player object (or its transform)
    [SerializeField] private Transform player;

    void Start(){
        //flip
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player reference is assigned
        if (player != null)
        {
            // Calculate direction to player
            Vector3 direction = player.position - transform.position;
            //direction.y = 0f; // Restrict rotation to the horizontal plane
            direction.y = direction.x + 0.4f;

            // Rotate the object to face the player's position
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.LogWarning("Player reference not assigned.");
        }
    }
}
