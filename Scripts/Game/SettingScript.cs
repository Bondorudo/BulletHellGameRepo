using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle fullScreenToggle;
    public int[] screenWidths;
    private int activeScreenResIndex;
    public GameObject settingsCanvas;
    public GameObject pauseMenu;

    private void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screenResIndex");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1)?true:false;
        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
        //mouseSensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity");

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }
        fullScreenToggle.isOn = isFullscreen;
    }

    public void Back()
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        settingsCanvas.SetActive(false);
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void SetScreenResolution(int i)
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16f / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screenResIndex", activeScreenResIndex);
            PlayerPrefs.Save(); 
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution (maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.MASTER);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.MUSIC);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.SFX);
    }

    public void SetMouseSensitivity(float value)
    {
        //MouseLook.instance.SetMouseSensitivity(value);
    }
}
