using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject creditsScreen, tutorialScreen;
    [SerializeField] private GameObject buttons;

    private MenuState menuState;
    private AsyncOperation levelAsync;
    private GameManager gameManager;
    private SceneLoader sceneLoader;

    public enum MenuState
    {
        MENU,
        CREDITS,
        TUTORIAL
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.PrepareLevel();
    }

    public void ShowCredits()
    {
        creditsScreen.SetActive(true);
        menuState = MenuState.CREDITS;
    }

    public void ShowTutorial()
    {
        tutorialScreen.SetActive(true);
        menuState = MenuState.TUTORIAL;
    }

    public void StartGame()
    {
        gameManager.Calibrate();
        buttons.SetActive(false);
        sceneLoader.StartLevel(SceneLoader.Scenes.MENU);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch(menuState)
            {
                case MenuState.TUTORIAL:
                    tutorialScreen.SetActive(false);
                    break;
                case MenuState.CREDITS:
                    creditsScreen.SetActive(false);
                    break;
                case MenuState.MENU:
                    AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                    activity.Call<bool>("moveTaskToBack", true);
                    break;
            }
        }
    }
}
