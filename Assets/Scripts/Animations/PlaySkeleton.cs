using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySkeleton : MonoBehaviour
{
    [SerializeField] private Sprite play;
    [SerializeField] private Sprite pause;
    [SerializeField] private Animator skeletonAnimator;
    [SerializeField] private AudioSource heartAudio;
    private SpriteRenderer spriteRenderer;
    private bool isPlaying = false;

    private void Update(){
        if(isPlaying){
            if(!heartAudio.isPlaying){
                spriteRenderer.sprite = play;
                skeletonAnimator.SetBool("Narrate", false);
                isPlaying = false;
            }
        }
    }

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void ChangeSprite()
    {
        if(spriteRenderer.sprite == play){
            //play skeleton narration
            spriteRenderer.sprite = pause;
            skeletonAnimator.SetBool("Narrate", true);

            heartAudio.Play();
            isPlaying = true;
        }else{
            //stop skeleton narration
            skeletonAnimator.SetBool("Narrate", false);
            spriteRenderer.sprite = play;
            heartAudio.Stop();
        }
    }
}
