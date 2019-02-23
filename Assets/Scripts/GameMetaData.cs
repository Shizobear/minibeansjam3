using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetaData
{
    private static int resolutionX = 512;
    private static int resolutionY = 448;
    private static int pixelsPerUnit = 16;

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
}