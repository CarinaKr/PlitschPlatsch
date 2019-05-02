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
    [SerializeField] private Button muteButton;
    [SerializeField] private Color muteColor, defaultColor;

    public float gameSpeed { get; set; }
    public Vector3 calibratedTilt { get; private set; }
    public bool isPlaying { get; set; }
    public int points { get; set; }

    private int highscore;
    private HighscoreManager highscoreManager;
    private SceneLoader sceneLoader;
    private bool isMute;
    

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
        CheckMute();
    }

    private void OnEnable()
    {
        BubbleManager.CollectBubble += CollectPoints;
    }
    private void OnDisable()
    {
        BubbleManager.CollectBubble -= CollectPoints;
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
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
        Time.timeScale = 1;
        sceneLoader.LoadGameOver();

        highscoreManager.UpdateHighscore(points);
    }

    public void Mute()
    {
        isMute = !isMute;
        int mute = isMute ? 0 : 1;
        PlayerPrefs.SetInt("Mute", mute);
        CheckMute();
    }

    private void CheckMute()
    {
        if (PlayerPrefs.HasKey("Mute"))
            isMute = PlayerPrefs.GetInt("Mute") == 0 ? true : false;
        else
            isMute = false;

        if (isMute)
            muteButton.GetComponent<Image>().color = muteColor;
        else
            muteButton.GetComponent<Image>().color = defaultColor;
    }
}
