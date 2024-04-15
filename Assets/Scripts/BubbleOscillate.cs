using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    private const float movementSpeed = 0.005f;

    // These are very magical numbers in the inspector, I cannot figure out any rhyme or reason behind it, it's also not relative?
    // TLDR; how did you come to these numbers and why? How would you configure these numbers for new future bubbles?
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private bool movingUp = true;

    private void Update()
    {
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