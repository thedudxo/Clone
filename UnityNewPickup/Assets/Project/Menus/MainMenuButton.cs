using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {

    public void ClonePlay()
    {
 //       SceneManager.LoadScene("Master Scene");
        SceneManager.LoadScene("DiscardTestingSceneOutdated");
    }

    public void CloneControls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloneQuit()
    {
        Application.Quit();
    }
}