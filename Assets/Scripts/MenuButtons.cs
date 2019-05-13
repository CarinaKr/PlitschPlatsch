using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject creditsScreen, tutorialScreen, exitScreen;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject[] creditsPageButtonTextPage1, creditsPageButtonTextPage2;
    [SerializeField] private GameObject creditsPage1, creditsPage2;

    private MenuState menuState;
    private AsyncOperation levelAsync;
    private GameManager gameManager;
    private SceneLoader sceneLoader;
    private int creditsPage;

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

    public void CreditsTogglePage()
    {
        creditsPage = (creditsPage + 1) % 2;
        
            foreach (GameObject obj in creditsPageButtonTextPage1)
                obj.SetActive(creditsPage==0? true:false);
            creditsPage1.SetActive(creditsPage == 0 ? true : false);

            foreach (GameObject obj in creditsPageButtonTextPage2)
                obj.SetActive(creditsPage == 0 ? false : true);
            creditsPage2.SetActive(creditsPage == 0 ? false : true);
        
    }

    public void ShowTutorial()
    {
        tutorialScreen.SetActive(true);
        menuState = MenuState.TUTORIAL;
    }

    public void StartGame()
    {
        gameManager.Calibrate();
        //buttons.SetActive(false);
        sceneLoader.StartLevel(SceneLoader.Scenes.MENU);
    }

    public void Back()
    {
        switch (menuState)
        {
            case MenuState.TUTORIAL:
                tutorialScreen.SetActive(false);
                break;
            case MenuState.CREDITS:
                creditsScreen.SetActive(false);
                break;
        }
        menuState = MenuState.MENU;
    }

    public void Quit(bool exit)
    {
        if (exit)
            Application.Quit();
        else
            exitScreen.SetActive(false);
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
                    //AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                    //activity.Call<bool>("moveTaskToBack", true);
                    exitScreen.SetActive(true);
                    break;
            }
            menuState = MenuState.MENU;
        }
    }
}
