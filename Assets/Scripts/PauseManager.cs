using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private bool isPaused;
    private GameManager gameManager;
    private SceneLoader sceneLoader;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Continue()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        sceneLoader.BackToMenu(SceneLoader.Scenes.LEVEL);
        gameManager.gameSpeed = 1;
    }

    public void Recalibrate()
    {
        gameManager.Calibrate();
        Continue();
    }
}
