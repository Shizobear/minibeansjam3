using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement
{
    private Fish fish;

    public FishMovement(Fish _fish)
    {
        fish = _fish;
    }

    private Vector3 inputVector;
    private float x = 0;
    private float y = 0;
    public void UpdateInput()
    {
        inputVector = Vector3.zero;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        inputVector.x = x;
        inputVector.y = y;

        fish.Move(inputVector);

    }
}