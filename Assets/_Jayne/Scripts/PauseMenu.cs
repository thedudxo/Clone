using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public GameObject winScreen;
    public KeyCode pauseKey;
    public Texture2D crossHair;
    Vector2 hotSpot = Vector2.zero;
    AudioSource pauseMenuSource;
    AudioSource winMenuSource;
    public Slider gameVolSlider;
    public GameObject winCollider;

    // Use this for initialization
    void Start() {
        pauseMenuSource = gameObject.GetComponent<AudioSource>();
        winMenuSource = winScreen.GetComponent<AudioSource>();
        WinTrigger.isWin = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
            pauseMenuSource.ignoreListenerPause = true;
            pauseMenuSource.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Time.timeScale = 0.0f;
        }

        if (WinTrigger.isWin)
        {
            WinScreen();
        }
    }

    public void GameResume()
    {
 //      Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(crossHair, Vector2.zero, CursorMode.Auto);
        AudioListener.pause = false;
        pauseMenuSource.Stop();
        Time.timeScale = 1.0f;
//       AudioManager.instance.SetGameVolume(gameVolSlider.value);
 //      AudioManager.instance.Play("Theme");
 //      AudioManager.instance.Play("Wind");
        pauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Time.timeScale = 1.0f;
        WinTrigger.isWin = false;
    }

    void WinScreen()
    {
        winScreen.SetActive(true);
        winMenuSource = winScreen.GetComponent<AudioSource>();
        AudioListener.pause = true;
        winMenuSource.ignoreListenerPause = true;
        winMenuSource.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Time.timeScale = 0.0f;
    }

    public void ReallyQuit()
    {
        Application.Quit();
    }
}
