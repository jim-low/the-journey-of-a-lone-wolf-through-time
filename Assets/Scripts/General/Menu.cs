using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public Slider BGMSlider;

    // TODO: find out how to set game sound effect
    public Slider soundEffectSlider;

    public AudioSource music;

    bool settingsOn = false;

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
