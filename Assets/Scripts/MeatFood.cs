using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatFood : Collectable
{
    [SerializeField]
    private float bonusWeight = 1f;

    public override void OnCollect(iCollectorFish _collector)
    {
        _collector.IncreaseWeight(bonusWeight);
        this.gameObject.SetActive(false);
    }
}