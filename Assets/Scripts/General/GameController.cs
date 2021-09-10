using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject pauseMenu;
    bool paused = false;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        paused = !paused;
        if (paused) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

        pauseMenu.SetActive(paused);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
