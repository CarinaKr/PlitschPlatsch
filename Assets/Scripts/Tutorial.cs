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
        yield return new WaitForSeconds(delayToStart);
        SwitchStep(TutorialStep.TILT);
        yield return new WaitForSeconds(timeShowTilt);
        SwitchStep(TutorialStep.IDLE);
        yield return new WaitForSeconds(delayToSensor);
        SwitchStep(TutorialStep.SENSOR);
        yield return new WaitForSeconds(timeShowSensor);
        SwitchStep(TutorialStep.IDLE);
        TutorialDone.Invoke(true);
    }
}
