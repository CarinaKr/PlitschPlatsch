using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public bool isNewHighscore { get; private set; }
    public int currentHighscore { get; private set; }

    [SerializeField] private Text highscoreText;
    [SerializeField] private Color newHighscoreColor,defaultHighscoreColor;
    
    private AudioHandler audioHandler;

    // Start is called before the first frame update
    void Start()
    {
        audioHandler = FindObjectOfType<AudioHandler>();

        if (PlayerPrefs.HasKey("Highscore"))
            currentHighscore = PlayerPrefs.GetInt("Highscore");
        else
            currentHighscore = 0;

        highscoreText.text = "Highscore: " + currentHighscore;
        //ClearPlayerPrefs();
    }
    
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UpdateHighscore(int newPoints)
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            int oldHighscore = PlayerPrefs.GetInt("Highscore");
            if (oldHighscore < newPoints)
            {
                NewHighscore(newPoints);
            }
        }
        else
            NewHighscore(newPoints);

    }

    private void NewHighscore(int points)
    {
        PlayerPrefs.SetInt("Highscore", points);
        //highscoreText.color = newHighscoreColor;
        highscoreText.text = "Highscore: " + points;
        audioHandler.NewHighscore();
        isNewHighscore = true;
    }

    public void ResetTextColor()
    {
        //highscoreText.color = defaultHighscoreColor;
        isNewHighscore = false;
    }
}
