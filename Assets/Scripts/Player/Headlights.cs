using System;
using System.Collections;
using System.Collections.Generic;
using UnityAndroidSensors.Scripts.Utils.SmartVars;
using UnityEngine;
using UnityEngine.UI;

public class Headlights : MonoBehaviour
{
    public enum INPUT_TYPE
    {
        LIGHT,
        PROXIMITY
    }

    public static Action<bool> onSwitchLight;
    public bool isLightOn { get; private set; } 

    [SerializeField] private Slider energySlider;
    [SerializeField] private SpriteRenderer headlightsRenderer;
    [SerializeField] private float maxEnergy, loseEnergySpeed, gainEnergySpeed;
    [SerializeField] private FloatVar lightVar, proximityVar;
    [SerializeField] INPUT_TYPE inputType; 


    private GameManager gameManger;
    private float energyLeft;
    private FloatVar inputVar;

    // Start is called before the first frame update
    void Start()
    {
        gameManger = FindObjectOfType<GameManager>();
        energyLeft = maxEnergy;
        switch (inputType)
        {
            case INPUT_TYPE.LIGHT:
                inputVar = lightVar;
                break;
            case INPUT_TYPE.PROXIMITY:
                inputVar = proximityVar;
                break;
        }

        if (inputVar.value > 5)
        {
            SwitchOn();
        }
        else
            SwitchOff();
    }

    // Update is called once per frame
    void Update()
    {
        //switch (inputType)
        //{
        //    case INPUT_TYPE.LIGHT:
        //        debugText.text = "" + lightVar.value;
        //        break;
        //    case INPUT_TYPE.PROXIMITY:
        //        debugText.text = "" + proximityVar.value;
        //        break;
        //}
        if (!gameManger.isPlaying)
            return;

        if (isLightOn || inputVar.value>=5f)
        {
            if (!isLightOn) SwitchOn();
            energyLeft -= loseEnergySpeed * Time.deltaTime ; 
            if (energyLeft <= 0)
            {
                energyLeft = 0;
                SwitchOff();
            }
        }
        else if(!isLightOn && energyLeft<maxEnergy)
        {
            energyLeft = Mathf.Min(energyLeft + (gainEnergySpeed * Time.deltaTime), maxEnergy);
        }

        energySlider.value = energyLeft / maxEnergy;
    }

    public void SwitchOn()
    {
        isLightOn = true;
        headlightsRenderer.enabled = true;

        if(onSwitchLight!=null)
        {
            onSwitchLight.Invoke(true);
        }
    }

    public void SwitchOff()
    {
        isLightOn = false;
        headlightsRenderer.enabled = false;

        if (onSwitchLight != null)
        {
            onSwitchLight.Invoke(false);
        }

    }
}
