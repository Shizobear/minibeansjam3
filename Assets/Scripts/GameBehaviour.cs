using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour

{
    protected GameManager gameManager;
    public virtual void Awake()
    {
        gameManager = GameManager.GetInstance();
        gameManager.Subscribe(this);
    }
    public virtual void UpdateBehaviour()
    {

    }

    private void OnDestroy()
    {
        gameManager.Unsubscribe(this);
    }
}