using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed: MonoBehaviour
{
    [SerializeField] float speedIncrease, speedIncreaseDelay;

    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>speedIncreaseDelay)
        {
            Time.timeScale += speedIncrease;
            time = 0;
        }
    }
}
