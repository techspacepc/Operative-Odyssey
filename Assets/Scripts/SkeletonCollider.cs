using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCollider : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!audioSource.isPlaying){
                audioSource.Play();
            }

            Debug.Log("TRIGGER");
        }
    }
}
