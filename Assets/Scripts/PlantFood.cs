using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFood : Collectable
{
    [SerializeField]
    private float weightBonus = 0.5f;
    private float spawn_interval = 15f;
    public override void OnCollect(iCollectorFish _collector)
    {
        if (_collector is Piranha)
            return;

        _collector.IncreaseWeight(weightBonus);
        this.gameObject.SetActive(false);
    }
}