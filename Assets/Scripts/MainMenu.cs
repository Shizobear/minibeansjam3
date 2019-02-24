using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        // Screen.SetResolution(1155, 638, true);
    }
    public void PlayGame()
    {

        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {

        Debug.Log("ICH WURDE BEENDET!");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}

