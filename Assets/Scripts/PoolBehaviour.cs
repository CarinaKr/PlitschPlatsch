using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehaviour : MonoBehaviour
{
    [SerializeField] int poolSize;
    [SerializeField] private GameObject oxygenBubblePrefab;

    private List<GameObject> bubbles;

    // Start is called before the first frame update
    void Start()
    {
        bubbles = new List<GameObject>();

        for(int i=0;i<poolSize;i++)
        {
            GameObject newBubble = Instantiate(oxygenBubblePrefab, transform);
            bubbles.Add(newBubble);
            newBubble.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject bubble in bubbles)
        {
            if (bubble.activeSelf)
                return bubble;
        }

        return null;
    }

    public void ReleaseObject(GameObject bubble)
    {
        if (bubbles.Contains(bubble))
            bubble.SetActive(false);
    }
}
