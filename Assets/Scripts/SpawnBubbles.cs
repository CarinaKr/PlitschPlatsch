using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubbles : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] [Range(0, 1)] private float chanceDarkActive, chanceSameHeight;

    private PoolBehaviour spawnPositionsPool;
    private GameManager gameManager;
    private int lastPosition;
    private float time=Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawnPositionsPool = FindObjectOfType<PoolBehaviour>();

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
        int nextPosDifF;
        float randValue = Random.Range(0f,1f);
        if (lastPosition==0)
        {
            if (randValue < chanceSameHeight)
                nextPosDifF = 0;
            else
                nextPosDifF = 1;
        }
        else if(lastPosition==spawnPositions.Length-1)
        {
            if (randValue < chanceSameHeight)
                nextPosDifF = 0;
            else
                nextPosDifF = -1;
        }
        else
        {
            if (randValue < chanceSameHeight)
                nextPosDifF = 0;
            else
                nextPosDifF = Random.Range(0, 2) == 0 ? -1 : 1;
        }

        GameObject nextBubble=spawnPositionsPool.GetObject();
        nextBubble.transform.position = spawnPositions[lastPosition + nextPosDifF].position;

        float active = Random.Range(0f, 1f);
        nextBubble.GetComponent<BubbleManager>().isLightActive = active <= chanceDarkActive ? false : true;

        lastPosition = lastPosition + nextPosDifF;
    }
}
