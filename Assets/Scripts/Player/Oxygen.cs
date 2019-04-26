using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private float maxOxygen, loseOxygenSpeed;
    [SerializeField] private float oxygenPerBubble;
    [SerializeField] private float multiplyFactor;

    private GameManager gameManager;
    private float oxygenLeft;

    private void OnEnable()
    {
        BubbleManager.CollectBubble += AddOxygen;
    }
    private void OnDisable()
    {
        BubbleManager.CollectBubble -= AddOxygen;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        oxygenLeft = maxOxygen;
    }

    private void Update()
    {
        if (!gameManager.isPlaying)
            return;

        oxygenLeft -= loseOxygenSpeed * Time.deltaTime * multiplyFactor;
        if (oxygenLeft <= 0)
        {
            oxygenLeft = 0;
            gameManager.GameOver();
        }

        oxygenSlider.value = oxygenLeft / maxOxygen;
    }

    private void AddOxygen(GameObject bubble)
    {
        oxygenLeft = Mathf.Min(oxygenLeft + oxygenPerBubble, maxOxygen);
        
    }
    
}
