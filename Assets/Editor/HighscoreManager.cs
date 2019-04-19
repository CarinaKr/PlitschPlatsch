using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [MenuItem("Tools/PlayerPrefs/Clear all Player Prefs")]
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
        Debug.Log("new highscore");
    }
}
