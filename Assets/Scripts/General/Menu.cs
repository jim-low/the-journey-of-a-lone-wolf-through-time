using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private GameObject settingsMenu;
    private GameObject helpMenu;
    private GameObject mainMenu;

    public static int prevSceneIndex = 1;
    public Slider BGMSlider;
    public AudioSource music;

    bool settingsOn = false;
    bool helpOn = false;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    void Start()
    {
        mainMenu = GameObject.Find("MainMenuContainer");
        helpMenu = GameObject.Find("HowToPlayContainer");
        settingsMenu = GameObject.Find("SettingsContainer");

        if (mainMenu && helpMenu && settingsMenu) {
            helpMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Ground");
    }

    public void SetBGMVolume()
    {
        music.volume = BGMSlider.value;
    }

    public void ToggleHowToPlay()
    {
        helpOn = !helpOn;
        settingsOn = false;

        mainMenu.SetActive(!helpOn);
        helpMenu.SetActive(helpOn);
        settingsMenu.SetActive(false);
    }

    public void ToggleSettings()
    {
        settingsOn = !settingsOn;
        helpOn = false;

        mainMenu.SetActive(!settingsOn);
        helpMenu.SetActive(false);
        settingsMenu.SetActive(settingsOn);
    }

    public void Back()
    {
        settingsOn = false;
        helpOn = false;

        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TryAgain()
    {
        StartCoroutine(LoadPrevScene());
    }

    private IEnumerator LoadPrevScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(prevSceneIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
