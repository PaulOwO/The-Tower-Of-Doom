using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.transform.position = respawnPoint.transform.position;  // Respawn  
        anim.Play("Die");
    }
}

    
