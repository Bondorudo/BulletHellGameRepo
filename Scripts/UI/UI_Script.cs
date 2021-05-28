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
    public Button nextLevelButton;
    public Button continueButton;
    public Button restartButton;
    public Button quitButton;
    public Button saveButton;
    public Button loadButton;

    [Header("Menu Text")]
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI winText;

    [Header("UI Text")]
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI scoreText;

    private AudioManager audioManager;

    private int nextLevelToLoad;
    private int sceneCount;


    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        nextLevelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    public void PauseMenu()
    {
        pauseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        loadButton.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        loseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        loadButton.gameObject.SetActive(true);
    }

    public void Victory()
    {
        winText.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    public void ContinueButton()
    {
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelButton()
    {
        audioManager.ButtonPressAudio();
        if (nextLevelToLoad < sceneCount)
        {
            SceneManager.LoadScene(nextLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitToMenu()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene("MainMenu");
    }
}
