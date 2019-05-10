using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager self;
    

    [SerializeField] private Text pointsText;
    [SerializeField] private int pointsPerBubble;
    [SerializeField] private int pointsMultiplyFactor;
    
    

    public float gameSpeed { get; set; }
    public Vector3 calibratedTilt { get; private set; }
    public bool isPlaying { get; set; }
    public int points { get; set; }
    public bool isTutorialDone { get; set; }

    private int highscore;
    private HighscoreManager highscoreManager;
    private SceneLoader sceneLoader;
    

    private void Awake()
    {
        if (self == null)
            self = this;
        else if (self != this)
            Destroy(gameObject);
        
    }

    private void Start()
    {
        highscoreManager = GetComponent<HighscoreManager>();
        sceneLoader=GetComponent<SceneLoader>();

        gameSpeed = 1;
        
    }

    private void OnEnable()
    {
        Bubble.CollectBubble += CollectPoints;
        Tutorial.TutorialDone += OnTutorialDone;
    }
    private void OnDisable()
    {
        Bubble.CollectBubble -= CollectPoints;
        Tutorial.TutorialDone -= OnTutorialDone;
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        isTutorialDone = false;
        points = 0;
        pointsText.text = "Points: " + points;
    }

    public void CollectPoints(GameObject obj)
    {
        //points += pointsPerBubble;
        points += Mathf.RoundToInt(Time.timeScale * pointsMultiplyFactor);
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
        gameSpeed = 1;
        sceneLoader.LoadGameOver();

        highscoreManager.UpdateHighscore(points);
    }

    private void OnTutorialDone(bool done)
    {
        isTutorialDone = done;
    }
}
