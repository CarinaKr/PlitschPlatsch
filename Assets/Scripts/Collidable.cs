﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer foreground;
    [SerializeField] protected float releaseDelay;

    protected PoolBehaviour pool;
    protected bool isCollided;

    protected abstract void TriggerCollision();

    protected void Start()
    {
        pool = GetComponentInParent<PoolBehaviour>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && foreground.enabled)
        {
            isCollided = true;
            TriggerCollision();
            animator.SetTrigger("Attack");
            StartCoroutine("ReleaseObject", gameObject);
        }
    }

    private IEnumerator ReleaseObject(GameObject obj)
    {
        yield return new WaitForSeconds(releaseDelay);
        isCollided = false;
        pool.ReleaseObject(obj);
    }

    
}
