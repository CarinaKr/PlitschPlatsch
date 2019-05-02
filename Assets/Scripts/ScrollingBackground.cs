﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float leftBorder;
    [SerializeField] private float tileWidth;
    [SerializeField] private float startSpeed;

    private Rigidbody2D[] backgroundTiles;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        backgroundTiles = GetComponentsInChildren<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Rigidbody2D rb in backgroundTiles)
        {
            rb.velocity = new Vector2(startSpeed*-1*Time.deltaTime*gameManager.gameSpeed, 0);
            if(rb.transform.position.x<leftBorder)
            {
                rb.transform.position = new Vector2(rb.transform.position.x+(tileWidth*2),0);
            }
        }
    }
}