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

    public Slider BGMSlider;
    // TODO: find out how to set game sound effect
    public Slider soundEffectSlider;

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
        helpMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Ground");
    }

    public void SetBGMVolume()
    {
        music.volume = BGMSlider.value;
    }

    public void SetSoundEffectVolume()
    {
    }

    public void ToggleHowToPlay()
    {
        helpOn = !helpOn;

        helpMenu.SetActive(helpOn);
        mainMenu.SetActive(!helpOn);
    }

    public void ToggleSettings()
    {
        settingsOn = !settingsOn;

        settingsMenu.SetActive(settingsOn);
        mainMenu.SetActive(!settingsOn);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
