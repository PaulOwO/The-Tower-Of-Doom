﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
