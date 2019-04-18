using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager self;

    [SerializeField] private GameObject startButton;

    public Vector3 calibratedTilt { get; private set; }
    public bool isPlaying { get; set; }

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

    public void Calibrate()
    {
        calibratedTilt = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        startButton.SetActive(false);
        isPlaying = true;
    }
}
