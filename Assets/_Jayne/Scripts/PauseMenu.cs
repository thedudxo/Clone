﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject WinAudioHolder;

    public KeyCode pauseKey;
    AudioSource pauseMenuSource;
    AudioSource winMenuSource;
 //   public Slider gameVolSlider;
    public GameObject winCollider;

    void Start() {
        pauseMenuSource = gameObject.GetComponent<AudioSource>();
        winMenuSource = WinAudioHolder.GetComponent<AudioSource>();
        WinTrigger.isWin = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            AudioListener.pause = true;
            pauseMenuSource.ignoreListenerPause = true;
            pauseMenuSource.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (WinTrigger.isWin)
        {
            WinScreen();
        }
    }

    public void GameResume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        pauseMenuSource.Stop();
//       AudioManager.instance.SetGameVolume(gameVolSlider.value);
 //      AudioManager.instance.Play("Theme");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = false;
        Time.timeScale = 1.0f;
        WinTrigger.isWin = false;
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
 //       winMenuSource = winScreen.GetComponent<AudioSource>();
        AudioListener.pause = true;
        winMenuSource.ignoreListenerPause = true;
        winMenuSource.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
    }

    public void ReallyQuit()
    {
        Application.Quit();
    }
}
