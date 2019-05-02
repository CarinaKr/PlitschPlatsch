using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static event Action<GameObject> CollectBubble;
    public bool _isLightActive;

    [SerializeField] private float leftBorder;
    [SerializeField] Sprite lightBubble, darkBubble;
    [SerializeField] SpriteRenderer foreground;
    [SerializeField] private float speed;

    private PoolBehaviour pool;
    private GameManager gameManager;
    private Headlights headlights;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        Headlights.onSwitchLight += SwitchLight;
    }
    private void OnDisable()
    {
        Headlights.onSwitchLight -= SwitchLight;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pool = GetComponentInParent<PoolBehaviour>();
        
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPlaying)
            return;

        rb.velocity = new Vector2(speed * -1 * Time.deltaTime*gameManager.gameSpeed, 0);
        if (rb.transform.position.x < leftBorder)
        {
            pool.ReleaseObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Player" && foreground.enabled)
        {
            CollectBubble.Invoke(gameObject);
        }
    }

    private void SwitchLight(bool isLightOn)
    {
        if ((isLightActive && isLightOn) || (!isLightActive && !isLightOn))
        {
            foreground.enabled = true;
        }
        else
            foreground.enabled = false;
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

            if (headlights == null)
                headlights = FindObjectOfType<Headlights>();

            foreground.sprite = isLightActive ? lightBubble : darkBubble;
            SwitchLight(headlights.isLightOn);
        }
    }
}
