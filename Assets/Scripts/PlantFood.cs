using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFood : Collectable
{
    [SerializeField]
    private float weightBonus = 0.5f;
    public override void OnCollect(iCollectorFish _collector)
    {
        _collector.IncreaseWeight(weightBonus);
        this.gameObject.SetActive(false);
    }
}