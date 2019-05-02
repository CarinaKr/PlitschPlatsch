using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public static event Action<GameObject> CollectBubble;
    public bool _isLightActive;

    [SerializeField] private float leftBorder;
    [SerializeField] private Sprite lightBubbleForeground,lightBubbleBackground, darkBubbleForeground,darkBubbleBackground;
    [SerializeField] private SpriteRenderer foreground, background;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private float maxLightAngle;

    private PoolBehaviour pool;
    private GameManager gameManager;
    private Headlights headlights;
    private Rigidbody2D rb;
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
        if(/*!isLightActive&&*/!isInCone)
        {
            foreground.enabled = false;
            background.enabled = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Player" && foreground.enabled)
        {
            CollectBubble.Invoke(gameObject);
            animator.SetTrigger(isLightActive ? "LightPlop" : "DarkPlop");
            StartCoroutine("ReleaseBubble",gameObject);
        }
    }

    private void SwitchLight(bool isLightOn)
    {
        if ((isLightActive && isLightOn && isInCone) || (!isLightActive && !isLightOn && isInCone))
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

            //if (headlights == null)
            //{
            //    headlights = FindObjectOfType<Headlights>();
            //}

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
            if (/*transform.position.x>headlights.transform.position.x &&*/ angle<maxLightAngle)
            {
                return true;
            }

            return false;
        }
    }

    private IEnumerator ReleaseBubble(GameObject bubble)
    {
        yield return new WaitForSeconds(0.5f);
        pool.ReleaseObject(bubble);
    }
}
