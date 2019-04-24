using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager self;

    public float gameSpeed;

    [SerializeField] private GameObject startButton;
    [SerializeField] private Text pointsText;
    [SerializeField] private int pointsPerBubble;
    [SerializeField] private GameObject pauseScreen;

    public Vector3 calibratedTilt { get; private set; }
    public bool isPlaying { get; set; }
    public bool isPaused { get; set; }
    public int points { get; set; }

    private int highscore;
    

    private void Awake()
    {
        if (self == null)
            self = this;
        else if (self != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    private void OnEnable()
    {
        BubbleManager.CollectBubble += CollectPoints;
    }
    private void OnDisable()
    {
        BubbleManager.CollectBubble -= CollectPoints;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { Pause(); }
    }

    public void CollectPoints(GameObject obj)
    {
        points += pointsPerBubble;
        pointsText.text = "Points: " + points;
    }

    public void Calibrate()
    {
        calibratedTilt = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        startButton.SetActive(false);
        isPlaying = true;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        isPaused = true;
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        isPlaying = true;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
