using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public KeyCode pauseKey;
    AudioSource pauseMenuSource;

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
        pauseMenu.SetActive(false);
        pauseMenuSource.Stop();
        Time.timeScale = 1.0f;
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
