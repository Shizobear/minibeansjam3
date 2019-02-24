using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
public class PixelPerfectInitalizer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitPixelCamera()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(GameMetaData.ResolutionX, GameMetaData.ResolutionY, true, 60);


        // PixelPerfectCamera camera = Camera.main.GetComponent<PixelPerfectCamera>();
        // if (camera == null)
        //     camera = Camera.main.gameObject.AddComponent<PixelPerfectCamera>();

        // if (SceneManager.GetActiveScene().buildIndex != 0)
        // {
        //     camera.refResolutionX = GameMetaData.ResolutionX;
        //     camera.refResolutionY = GameMetaData.ResolutionY;
        // }

    }
}