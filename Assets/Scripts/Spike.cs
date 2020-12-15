using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using UnityEngine.SceneManagement;


public class Spike : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    //public Vector3 RespawnPos => respawnPoint.position;

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = respawnPoint.transform.position;  // Respawn  
        //anim.Play("Die");
    }

}

    
