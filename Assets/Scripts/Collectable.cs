using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Collectable : GameBehaviour
{
    public abstract void OnCollect(iCollectorFish _collector);

    protected void OnTriggerEnter2D(Collider2D other)
    {
        iCollectorFish _fish = other.GetComponent<iCollectorFish>();
        if (_fish != null)
            OnCollect(_fish);
    }
}