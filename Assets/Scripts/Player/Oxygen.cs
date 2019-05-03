using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private Image oxygenFillImage;
    [SerializeField] private float maxOxygen, loseOxygenSpeed;
    [SerializeField] private float oxygenPerBubble, oxygenPerEnemy;

    private GameManager gameManager;
    private float oxygenLeft;

    private void OnEnable()
    {
        Bubble.CollectBubble += AddOxygen;
        Enemy.CollideEnenmy += ReduceOxygen;
    }
    private void OnDisable()
    {
        Bubble.CollectBubble -= AddOxygen;
        Enemy.CollideEnenmy -= ReduceOxygen;
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

        oxygenLeft -= loseOxygenSpeed * Time.deltaTime * Mathf.Pow(gameManager.gameSpeed,2f);
        //oxygenLeft -= (loseOxygenSpeed + Mathf.Sqrt(gameManager.gameSpeed - 1)) * Time.deltaTime;
        if (oxygenLeft <= 0)
        {
            oxygenLeft = 0;
            gameManager.GameOver();
        }

        oxygenFillImage.fillAmount = oxygenLeft / maxOxygen;
    }

    private void AddOxygen(GameObject bubble)
    {
        oxygenLeft = Mathf.Min(oxygenLeft + oxygenPerBubble, maxOxygen);
        
    }

    private void ReduceOxygen(GameObject enemy)
    {
        oxygenLeft = Mathf.Max(oxygenLeft - oxygenPerEnemy, 0);

    }

}
