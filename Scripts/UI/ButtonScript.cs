using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private GameManager gm;
    private UI_Script uiScript;
    public GameObject settings;
    public GameObject pauseMenu;

    void Start()
    {
        // Setup
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
    }

    public void UI_Continue_Button()
    {
        Cursor.lockState = CursorLockMode.Confined;

        // if gametype is firstperson then lock cursor when continueing(??)
        if (gm.gameType == GameTypes.FIRSTPERSON || gm.gameType == GameTypes.SIDESCROLL)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        // play button press audio
        AudioManager.instance.PlaySound("UI_Buttons");
        gm.pauseGame = false;
        // Call disable UI function
        uiScript.SetDisableUI();
    }

    public void UI_RestartLevel_Button()
    {
        // play button press audio
        AudioManager.instance.PlaySound("UI_Buttons");
        // Find scene fader and fade to active scene to restart.
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
    }
    public void UI_Settings_Button()
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        pauseMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void UI_SaveAndQuit_Button()
    {
        // TODO: Save game
        // play button press audio
        AudioManager.instance.PlaySound("UI_Buttons");
        // Find scene fader and fade to main menu
        FindObjectOfType<SceneFader>().FadeTo("MainMenu");
    }
}
