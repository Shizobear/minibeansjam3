using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetaData
{
    private static int resolutionX = 512;
    private static int resolutionY = 448;
    private static int pixelsPerUnit = 16;

    private static int max_amount_of_hooks = 4;

    private static float hook_cooldown = 10f;

    public static int ResolutionX
    {
        get
        {
            return resolutionX;
        }


    }

    public static int ResolutionY
    {
        get
        {
            return resolutionY;
        }

    }

    public static int PixelsPerUnit
    {
        get
        {
            return pixelsPerUnit;
        }

    }

    public static int Max_amount_of_hooks
    {
        get
        {
            return max_amount_of_hooks;
        }

    }

    public static float Hook_cooldown
    {
        get
        {
            return hook_cooldown;
        }

    }
}