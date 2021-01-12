using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] AudioSource myFx;
    [SerializeField] AudioClip hoverFx;
    [SerializeField] AudioClip clickFx;

    private void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    private void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}

