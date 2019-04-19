using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubbles : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] [Range(0,1)] private float chanceDarkActive;

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

        if(time>=spawnDelay)
        {
            Spawn();
            time = 0;
        }
        time += Time.deltaTime;
    }

    public void Spawn()
    {
        int nextPosDifF;
        if(lastPosition==0)
            nextPosDifF = Random.Range(0, 2);
        else if(lastPosition==spawnPositions.Length-1)
            nextPosDifF = Random.Range(-1, 1);
        else
            nextPosDifF = Random.Range(-1, 2);

        GameObject nextBubble=spawnPositionsPool.GetObject();
        nextBubble.transform.position = spawnPositions[lastPosition + nextPosDifF].position;

        float active = Random.Range(0f, 1f);
        nextBubble.GetComponent<BubbleManager>().isLightActive = active <= chanceDarkActive ? false : true;
    }
}
