using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMovement : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float leftBorder;

    protected PoolBehaviour pool;
    protected GameManager gameManager;
    protected Rigidbody2D rb;

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

        rb.velocity = new Vector2(speed * -1 * Time.deltaTime * gameManager.gameSpeed, 0);
        if (rb.transform.position.x <= leftBorder)
        {
            Reuse();
        }
    }

    protected virtual void Reuse()
    {
        pool.ReleaseObject(gameObject);
    }
}
