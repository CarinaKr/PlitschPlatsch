using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Collidable
{
    public static event Action<GameObject> CollectBubble;
    public bool _isLightActive;

    [SerializeField] private Sprite lightBubbleForeground,lightBubbleBackground, darkBubbleForeground,darkBubbleBackground;
    [SerializeField] private SpriteRenderer  background;
    [SerializeField] private float maxLightAngle;
   
    private Headlights headlights;
    private Transform headlightsPosition;

    //private bool isInCone;

    private void OnEnable()
    {
        Headlights.onSwitchLight += SwitchLight;
        headlights = FindObjectOfType<Headlights>();
    }
    private void OnDisable()
    {
        Headlights.onSwitchLight -= SwitchLight;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isLightActive && !isInCone && !isCollided)
        {
            foreground.enabled = false;
            background.enabled = true;
        }
    }

    protected override void TriggerCollision()
    {
        CollectBubble.Invoke(gameObject);
    }

    private void SwitchLight(bool isLightOn)
    {
        if ((isLightActive && isLightOn && isInCone) || (!isLightActive && !isLightOn))
        {
            foreground.enabled = true;
            background.enabled = false;
        }
        else
        {
            foreground.enabled = false;
            background.enabled = true;
        }
    }
    
    public bool isLightActive
    {
        get
        {
            return _isLightActive;
        }
        set
        {
            _isLightActive = value;

            foreground.sprite = isLightActive ? lightBubbleForeground : darkBubbleForeground;
            background.sprite = isLightActive ? lightBubbleBackground : darkBubbleBackground;
            animator.SetTrigger(isLightActive ? "Light" : "Dark");
            SwitchLight(headlights.isLightOn);
        }
    }

    private bool isInCone
    {
        get
        {
            float angle = Vector2.Angle(transform.right, (transform.position-headlights.transform.position).normalized);
            if (angle<maxLightAngle)
            {
                return true;
            }

            return false;
        }
    }

    
}
