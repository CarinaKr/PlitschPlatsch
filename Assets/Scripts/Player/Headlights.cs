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

    [SerializeField] private Slider energySlider;
    [SerializeField] private SpriteRenderer headlightsRenderer;
    [SerializeField] private float maxEnergy, loseEnergySpeed, gainEnergySpeed;
    [SerializeField] private Text debugText;
    [SerializeField] private FloatVar lightVar, proximityVar;
    [SerializeField] INPUT_TYPE inputType; 


    private GameManager gameManger;
    private bool isOn;
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
    }

    // Update is called once per frame
    void Update()
    {
        switch (inputType)
        {
            case INPUT_TYPE.LIGHT:
                debugText.text = "" + lightVar.value;
                break;
            case INPUT_TYPE.PROXIMITY:
                debugText.text = "" + proximityVar.value;
                break;
        }
        if (!gameManger.isPlaying)
            return;

        if (isOn || inputVar.value>=5f)
        {
            energyLeft -= loseEnergySpeed * Time.deltaTime; 
            if (energyLeft <= 0)
            {
                energyLeft = 0;
                SwitchOff();
            }
        }
        else if(!isOn && energyLeft<maxEnergy)
        {
            energyLeft = Mathf.Min(energyLeft + (gainEnergySpeed * Time.deltaTime), maxEnergy);
        }

        energySlider.value = energyLeft / maxEnergy;
    }

    public void SwitchOn()
    {
        isOn = true;
        headlightsRenderer.enabled = true;
    }

    public void SwitchOff()
    {
        isOn = false;
        headlightsRenderer.enabled = false;
    }
}
