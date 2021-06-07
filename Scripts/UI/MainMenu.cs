using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject newGameButton;
    public GameObject continueButton;

    private void Start()
    {
        // Set time scale to 1 just incase.
        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("levelReached") > 1)
        {
            newGameButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            newGameButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (settingsMenu != null)
        {
            if (!settingsMenu.activeSelf)
            {
                mainMenu.gameObject.SetActive(true);
            }
        }
    }

    public void ContinueGame()
    {
        int highestLevel = PlayerPrefs.GetInt("levelReached", 1);
        AudioManager.instance.PlaySound("UI_Buttons");
        FindObjectOfType<SceneFader>().FadeTo("Level_" + highestLevel);
    }

    // Find scene fader and fade to specified scene.
    public void Play(string sceneToLoad)
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void SettingsMenu()
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    // Quit the application
    public void Quit()
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        Debug.Log("Exiting");
        Application.Quit();
    }
}
