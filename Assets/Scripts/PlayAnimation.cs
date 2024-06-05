using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator;
    public AudioClip clip;
    public AudioSource source;

    public void PlayCrashAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Crash");
            if (clip != null && source != null)
            {
                source.PlayOneShot(clip);
            }
        }
    }
}
