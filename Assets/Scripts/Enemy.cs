using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Collidable
{
    public static event Action<GameObject> CollideEnenmy;


    protected override void TriggerCollision()
    {
        CollideEnenmy.Invoke(gameObject);
    }

}
