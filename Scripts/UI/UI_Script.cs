using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    // References to UI elements
    [Header("Menu Buttons")]
    public Button continueButton;
    public Button restartButton;
    public Button saveAndQuitButton;

    [Header("Menu Text")]
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI winCountDown;
    public TextMeshProUGUI deathText;

    [Header("UI Text")]
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI scoreText;

    private AudioManager audioManager;

    private int nextLevelToLoad;
    private int sceneCount;

    private TopFirstPersonGameManager gm;
    private int countDownTime = 3;


    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<TopFirstPersonGameManager>();
        nextLevelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        sceneCount = SceneManager.sceneCountInBuildSettings;
        StartCoroutine(CountDownToNextLevel());
    }

    IEnumerator CountDownToNextLevel()
    {   
        // TODO: fix countdown
        while (1 == 1)
        {
            while (winCountDown.IsActive() && countDownTime >= 0)
            {
                winCountDown.text = countDownTime.ToString();
                yield return new WaitForSeconds(1f);
                countDownTime--;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void PauseMenu()
    {
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        saveAndQuitButton.gameObject.SetActive(true);
    }

    public void Victory()
    {
        winCountDown.gameObject.SetActive(true);

        if (countDownTime <= 0)
        {
            if (nextLevelToLoad < sceneCount)
            {
                SceneManager.LoadScene(nextLevelToLoad);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void GameOver()
    {
        deathText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        saveAndQuitButton.gameObject.SetActive(true);
    }

    public void ContinueButton()
    {
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        saveAndQuitButton.gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene("MainMenu");
    }
}
