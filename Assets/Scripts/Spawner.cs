using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private Transform[] spawnPositions;
    //[SerializeField] private bool isBubble;
    [SerializeField] [Range(0, 1)] private float chanceDarkActive, chanceSameHeight;
    [SerializeField] [Range(0, 1)] private float chanceEnemy;
    [SerializeField]  private PoolBehaviour bubblesPool, enemiesPool;

    private GameManager gameManager;
    private int lastPosition;
    private float time=Mathf.Infinity;
    private bool isLastBubbleActive;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        lastPosition = spawnPositions.Length / 2;
    }

    private void Update()
    {
        if (!gameManager.isPlaying)
            return;

        if (time>=spawnDelay*(1/gameManager.gameSpeed))
        {
            Spawn();
            time = 0;
        }
        time += Time.deltaTime;
    }

    public void Spawn()
    {
        bool isBubble = Random.Range(0f, 1f) < chanceEnemy && gameManager.isTutorialDone ? false : true;
        GameObject nextObj = null;

        if (isBubble)
        {
            nextObj = bubblesPool.GetObject();
        }
        else
        {
            nextObj = enemiesPool.GetObject();
        }

        int nextPosDiff = GetNextPosDiff();
        nextObj.transform.position = spawnPositions[lastPosition + nextPosDiff].position;
        lastPosition = lastPosition + nextPosDiff;

        if (isBubble)
        {
            if (gameManager.isTutorialDone)
            {
                float active = Random.Range(0f, 1f);
                nextObj.GetComponent<Bubble>().isLightActive = active <= chanceDarkActive ? false : true;
            }
            else
            {
                nextObj.GetComponent<Bubble>().isLightActive = !isLastBubbleActive;
                isLastBubbleActive = !isLastBubbleActive;
            }
        }
    }

    private int GetNextPosDiff()
    {
        int nextPosDifF=0;
        float randValue = Random.Range(0f, 1f);
        if(randValue<chanceSameHeight)
        {
            nextPosDifF = 0;
        }
        else
        {
            if (lastPosition == 0)  //at the top                  
            {
                nextPosDifF = Random.Range(1, 3);
            }
            else if (lastPosition == 1) //second from top
            {
                nextPosDifF = Random.Range(0, 2) == 0 ? -1 : Random.Range(1, 3);
            }
            else if (lastPosition == spawnPositions.Length - 1) //at the bottom
            {
                nextPosDifF = Random.Range(-2, 0);
            }
            else if(lastPosition==spawnPositions.Length-2) //second from the bottom
            {
                nextPosDifF = Random.Range(0, 2) == 0 ? Random.Range(-2, 0) : 1;
            }
            else
            {
                nextPosDifF = Random.Range(0, 2) == 0 ? Random.Range(-2, 0) : Random.Range(1, 3);
            }
        }
        

        return nextPosDifF;
    }

    
}
