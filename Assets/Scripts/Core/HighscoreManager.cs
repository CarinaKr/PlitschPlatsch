using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private Text highscoreText;
    [SerializeField] private Color newHighscoreColor,defaultHighscoreColor;

    private int currentHighscore;
    private GameManager gameManager;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameManager = GetComponent<GameManager>();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        

        if (PlayerPrefs.HasKey("Highscore"))
            currentHighscore = PlayerPrefs.GetInt("Highscore");
        else
            currentHighscore = 0;

        highscoreText.text = "Highscore: " + currentHighscore;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == gameManager.menuSceneNumber)
            highscoreText.color = defaultHighscoreColor;
    }

    //[MenuItem("Tools/PlayerPrefs/Clear all Player Prefs")]
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
        highscoreText.color = newHighscoreColor;
        highscoreText.text = "Highscore: " + points;
    }
}
