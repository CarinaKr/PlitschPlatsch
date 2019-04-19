using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehaviour : MonoBehaviour
{
    [SerializeField] int poolSize;
    [SerializeField] private GameObject poolPrefab;

    private List<GameObject> objects;
    private Action onCollectBubble;
    

    private void OnEnable()
    {
        BubbleManager.CollectBubble += ReleaseObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();

        for(int i=0;i<poolSize;i++)
        {
            GameObject newObject = Instantiate(poolPrefab, transform);
            objects.Add(newObject);
            newObject.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject obj in objects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void ReleaseObject(GameObject bubble)
    {
        if (objects.Contains(bubble))
            bubble.SetActive(false);
    }
}
