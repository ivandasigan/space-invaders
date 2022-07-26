﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(2); // current game scene
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    public void NotGameOver()
    {
        _isGameOver = false;
    }
}
