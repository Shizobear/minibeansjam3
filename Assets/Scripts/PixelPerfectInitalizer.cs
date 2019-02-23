using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
public class PixelPerfectInitalizer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitPixelCamera()
    {

        PixelPerfectCamera camera = Camera.main.GetComponent<PixelPerfectCamera>();
        if (camera == null)
            camera = Camera.main.gameObject.AddComponent<PixelPerfectCamera>();

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            camera.refResolutionX = GameMetaData.ResolutionX;
            camera.refResolutionY = GameMetaData.ResolutionY;
        }

    }
}