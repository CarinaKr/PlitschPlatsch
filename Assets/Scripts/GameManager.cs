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

    public Vector3 calibratedTilt { get; private set; }
    public bool isPlaying { get; set; }
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CollectBubble()
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
}
