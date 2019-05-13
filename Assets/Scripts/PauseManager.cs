using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject[] tutorialScreens;
    [SerializeField] private Tutorial tutorial;


    private Dictionary<GameObject,bool> tutorialActive;
    private bool isPaused;
    private GameManager gameManager;
    private SceneLoader sceneLoader;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        tutorialActive = new Dictionary<GameObject, bool>();
        foreach (GameObject obj in tutorialScreens)
        {
            tutorialActive.Add(obj, obj.activeSelf);
        }
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
        if (!gameManager.isTutorialDone)
        {
            foreach (GameObject obj in tutorialScreens)
            {
                tutorialActive[obj]=obj.activeSelf;
                obj.SetActive(false);
            }
            tutorial.Pause();
        }
    }

    public void Continue()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        if (!gameManager.isTutorialDone)
        {
            tutorial.Continue();
            foreach (GameObject obj in tutorialScreens)
            {
                if (tutorialActive[obj])
                {
                    obj.SetActive(true);
                }
            }
        }
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
