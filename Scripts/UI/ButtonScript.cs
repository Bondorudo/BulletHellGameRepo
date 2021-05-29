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
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
    }

    public void UI_Continue_Button()
    {
        if (gm.gameType == GameTypes.FIRSTPERSON)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
        }
        audioManager.ButtonPressAudio();
        gm.pauseGame = false;
        uiScript.DisableUI();
    }

    public void UI_RestartLevel_Button()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UI_SaveAndQuit_Button()
    {
        // TODO: Save game
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene("MainMenu");
    }
}
