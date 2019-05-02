﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip bubblePlop;

    private AudioSource audioSource;

    private void OnEnable()
    {
        BubbleManager.CollectBubble += PlopBubble;
    }
    private void OnDisable()
    {
        BubbleManager.CollectBubble -= PlopBubble;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlopBubble(GameObject gameObject)
    {
        audioSource.clip = bubblePlop;
        audioSource.Play();
    }
}