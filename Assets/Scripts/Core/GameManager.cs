using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager self;

    public int  menuSceneNumber, gameSceneNumber, gameOverSceneNumber;

    [SerializeField] private Text pointsText;
    [SerializeField] private int pointsPerBubble;
    [SerializeField] private int multiplyFactor;

    public float gameSpeed {get;set;}
    public Vector3 calibratedTilt { get; private set; }
    public bool isPlaying { get; set; }
    public int points { get; set; }

    private int highscore;
    private HighscoreManager highscoreManager;
    

    private void Awake()
    {
        if (self == null)
            self = this;
        else if (self != this)
            Destroy(gameObject);

        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private void Start()
    {
        highscoreManager = GetComponent<HighscoreManager>();
    }

    private void OnEnable()
    {
        BubbleManager.CollectBubble += CollectPoints;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        BubbleManager.CollectBubble -= CollectPoints;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == menuSceneNumber || scene.buildIndex==gameSceneNumber)
        {
            Time.timeScale = 1;
            points = 0;
            pointsText.text = "Points: " + points;
        }
    }

    public void CollectPoints(GameObject obj)
    {
        //points += pointsPerBubble;
        points += Mathf.RoundToInt(Time.timeScale * multiplyFactor);
        pointsText.text = "Points: " + points;
    }

    public void Calibrate()
    {
        calibratedTilt = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        isPlaying = true;
    }

   
    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        isPlaying = false;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(gameSceneNumber);
        SceneManager.LoadScene(gameOverSceneNumber, LoadSceneMode.Additive);

        highscoreManager.UpdateHighscore(points);
    }
}
