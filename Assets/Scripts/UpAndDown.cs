using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float movementSpeed = 2f; // Adjust this value to change the speed of movement
    public float maxHeight = 5f; // Adjust this value to change the maximum height
    public float minHeight = 0f; // Adjust this value to change the minimum height

    private bool movingUp = true;

    void Update()
    {
        // Move the object up or down
        if (movingUp)
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.up);
            if (transform.localPosition.y >= maxHeight)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(movementSpeed * Time.deltaTime * Vector3.down);
            if (transform.localPosition.y <= minHeight)
            {
                movingUp = true;
            }
        }
    }
}