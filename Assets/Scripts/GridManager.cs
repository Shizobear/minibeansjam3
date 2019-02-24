using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    private static Grid grid;

    private void Awake()
    {
        grid = this.GetComponent<Grid>();
    }

    public static Grid GetGridReference()
    {
        return grid;
    }

}
