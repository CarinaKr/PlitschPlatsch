using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    private GameManager gameManager;
    private AsyncOperation levelAsync;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelAsync = SceneManager.LoadSceneAsync(gameManager.gameSceneNumber, LoadSceneMode.Additive);
        levelAsync.allowSceneActivation = false;
    }

    public void StartGame()
    {
        gameManager.Calibrate();
        SceneManager.UnloadSceneAsync(gameManager.gameOverSceneNumber);
        levelAsync.allowSceneActivation = true;
    }

    public void BackToMenu()
    {
        SceneManager.UnloadSceneAsync(gameManager.gameOverSceneNumber);
        SceneManager.LoadScene(gameManager.menuSceneNumber, LoadSceneMode.Additive);
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
