using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public KeyCode pauseKey;
    AudioSource pauseMenuSource;
    public Slider gameVolSlider;

    // Use this for initialization
    void Start () {
        pauseMenuSource = gameObject.GetComponent<AudioSource>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
            AudioManager.instance.Stop("Theme");
            AudioManager.instance.Stop("Wind");
            pauseMenuSource.ignoreListenerPause = true;
            pauseMenuSource.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
    }
    public void GameResume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        pauseMenuSource.Stop();
        Time.timeScale = 1.0f;
        Debug.Log("gameVolSlider.value is " + gameVolSlider.value);
        AudioManager.instance.SetGameVolume(gameVolSlider.value);
        AudioManager.instance.Play("Theme");
  //      AudioManager.instance.SetGameVolume(gameVolSlider.value, "Wind");
        AudioManager.instance.Play("Wind");
        pauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1.0f;
    }

    public void ReallyQuit()
    {
        Application.Quit();
    }

}
