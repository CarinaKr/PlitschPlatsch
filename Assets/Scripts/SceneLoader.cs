using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum Scenes
    {
        MENU,
        LEVEL,
        GAME_OVER
    }

    private static SceneLoader self;

    [SerializeField] private int menuSceneNumber, gameSceneNumber, gameOverSceneNumber;

    private GameManager gameManager;
    private HighscoreManager highscoreManager;
    private AsyncOperation levelAsync;

    private void Awake()
    {
        if (self == null)
            self = this;
        else if (self != this)
            Destroy(gameObject);

        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if(!gameManager) gameManager = GetComponent<GameManager>();
        if(!highscoreManager) highscoreManager = GetComponent<HighscoreManager>();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == menuSceneNumber || scene.buildIndex == gameSceneNumber)
        {
            gameManager.ResetGame();
        }

        if (scene.buildIndex == menuSceneNumber)
            highscoreManager.ResetTextColor();
    }

    public void LoadGameOver()
    {
        SceneManager.UnloadSceneAsync(gameSceneNumber);
        SceneManager.LoadScene(gameOverSceneNumber, LoadSceneMode.Additive);
    }

    public void PrepareLevel()
    {
        if (!SceneManager.GetSceneByBuildIndex(gameSceneNumber).isLoaded)
        {
            levelAsync = SceneManager.LoadSceneAsync(gameSceneNumber, LoadSceneMode.Additive);
            levelAsync.allowSceneActivation = false;
        }

    }

    public void StartLevel(Scenes scene)
    {
        switch(scene)
        {
            case Scenes.MENU:
                SceneManager.UnloadSceneAsync(menuSceneNumber);
                break;
            case Scenes.GAME_OVER:
                SceneManager.UnloadSceneAsync(gameOverSceneNumber);
                break;
        }
        levelAsync.allowSceneActivation = true;
    }

    public void BackToMenu()
    {
        //levelAsync.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync(gameSceneNumber);

        //SceneManager.UnloadSceneAsync(gameOverSceneNumber);
        //SceneManager.LoadScene(menuSceneNumber, LoadSceneMode.Additive);

        StartCoroutine("Back");
    }

    public IEnumerator Back()
    {
        levelAsync.allowSceneActivation = true;
        while(!levelAsync.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.UnloadSceneAsync(gameSceneNumber);
        SceneManager.UnloadSceneAsync(gameOverSceneNumber);
        SceneManager.LoadScene(menuSceneNumber, LoadSceneMode.Additive);
    }
}
