using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private AudioManager audioManager;
    private GameManager gm;
    private UI_Script uiScript;

    void Start()
    {
        // Setup
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
    }

    public void UI_Continue_Button()
    {
        // if gametype is firstperson then lock cursor when continueing(??)
        if (gm.gameType == GameTypes.FIRSTPERSON)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
        }
        // play button press audio
        audioManager.ButtonPressAudio();
        gm.pauseGame = false;
        // Call disable UI function
        uiScript.SetDisableUI();
    }

    public void UI_RestartLevel_Button()
    {
        // play button press audio
        audioManager.ButtonPressAudio();
        // Find scene fader and fade to active scene to restart.
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
    }

    public void UI_SaveAndQuit_Button()
    {
        // TODO: Save game
        // play button press audio
        audioManager.ButtonPressAudio();
        // Find scene fader and fade to main menu
        FindObjectOfType<SceneFader>().FadeTo("MainMenu");
    }
}
