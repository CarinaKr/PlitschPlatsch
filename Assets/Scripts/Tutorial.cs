using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{ 

    public enum TutorialStep
    {
        IDLE,
        TILT,
        SENSOR
    }

    public static event Action<bool> TutorialDone;

    [SerializeField] private GameObject tiltTutorial, sensorTutorial;
    [SerializeField] private float delayToStart, timeShowTilt, delayToSensor, timeShowSensor;

    private TutorialStep tutorialStep;
    private GameManager gameManager;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager.isTutorialDone)
        {
            tutorialStep = TutorialStep.IDLE;
            StartCoroutine("StartTutorial");
        }
    }

    public void SwitchStep(TutorialStep step)
    {
        switch(step)
        {
            case TutorialStep.IDLE:
                tiltTutorial.SetActive(false);
                sensorTutorial.SetActive(false);
                break;
            case TutorialStep.TILT:
                tiltTutorial.SetActive(true);
                sensorTutorial.SetActive(false);
                break;
            case TutorialStep.SENSOR:
                tiltTutorial.SetActive(false);
                sensorTutorial.SetActive(true);
                break;
        }

        tutorialStep = step;
    }

    private IEnumerator StartTutorial()
    {
        //yield return new WaitForSeconds(delayToStart);
        yield return Wait(delayToStart);
        SwitchStep(TutorialStep.TILT);
        //yield return new WaitForSeconds(timeShowTilt);
        yield return Wait(timeShowTilt);
        SwitchStep(TutorialStep.IDLE);
        //yield return new WaitForSeconds(delayToSensor);
        yield return Wait(delayToSensor);
        SwitchStep(TutorialStep.SENSOR);
        //yield return new WaitForSeconds(timeShowSensor);
        yield return Wait(timeShowSensor);
        SwitchStep(TutorialStep.IDLE);
        TutorialDone.Invoke(true);
    }

    private IEnumerator Wait(float time)
    {
        float totalTime = time;
        while(totalTime>0)
        {
            yield return new WaitForEndOfFrame();
            if(!isPaused)
                totalTime -= Time.deltaTime;
        }
        
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Continue()
    {
        isPaused = false;
    }
}
