using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudio : MonoBehaviour

{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource3;
    [SerializeField] AudioSource audioSource4;

    private void PlayWalkSound1()
    {
          audioSource.Play();
    }

    private void PlayWalkSound2()
    {
        audioSource1.Play();
    }

    private void PlayJumpSound()
    {
        audioSource3.Play();
    }

    private void PlayDieSound()
    {
        audioSource4.Play();
    }
}
