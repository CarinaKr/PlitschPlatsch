using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : ScrollingMovement
{
    [SerializeField] private float jumpTo;
    [SerializeField] private Transform otherTile;

    protected override void Reuse()
    {
        rb.transform.position = new Vector2(otherTile.position.x+jumpTo, 0);
    }
}
