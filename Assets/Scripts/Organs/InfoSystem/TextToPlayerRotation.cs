using Unity.VisualScripting;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetNextToHeartRight = 0.01f;
    [SerializeField] private float offsetNextToHeartLeft = 0.2f;
    private void Update()
    {
        //get bubble
        Transform bubble = transform.parent;

        //get heart
        Transform heart = bubble.parent;

        //left or right from the heart position based on z axis
        if(bubble.position.z > heart.position.z){
            //change text position based on heart and bubble position
            Vector3 newPosition = new Vector3(bubble.position.x, bubble.position.y, bubble.position.z + offsetNextToHeartRight);
            transform.position = newPosition;
        }else{
            //change text position based on heart and bubble position
            Vector3 newPosition = new Vector3(bubble.position.x, bubble.position.y, bubble.position.z - offsetNextToHeartLeft);
            transform.position = newPosition;
        }
    }
}