﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    private GameManager gameManager;
    private SceneLoader sceneLoader;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.PrepareLevel();
    }

    public void StartGame()
    {
        gameManager.Calibrate();
        sceneLoader.StartLevel(SceneLoader.Scenes.GAME_OVER);
    }

    public void BackToMenu()
    {
        sceneLoader.BackToMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
    }
}
