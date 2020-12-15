using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudio : MonoBehaviour

{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource3;
    [SerializeField] AudioSource audioSource4;

    public void PlayWalkSound1()
    {
          audioSource.Play();
    }

    public void PlayWalkSound2()
    {
        audioSource1.Play();
    }

    public void PlayJumpSound()
    {
        audioSource3.Play();
    }

    public void PlayDieSound()
    {
        audioSource4.Play();
    }
}
