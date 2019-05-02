using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed: MonoBehaviour
{
    [SerializeField] float speedIncrease, speedIncreaseDelay;

    private float time;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>speedIncreaseDelay)
        {
            //Time.timeScale += speedIncrease;
            gameManager.gameSpeed += speedIncrease;
            time = 0;
        }
    }
}
