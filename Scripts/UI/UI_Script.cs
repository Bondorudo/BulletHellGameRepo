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
    [Header("Menu Text")]
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI winCountDown;
    public TextMeshProUGUI deathText;

    [Header("Menu Buttons")]
    public Button continueButton;
    public Button restartButton;
    public Button settingsButton;
    public Button saveAndQuitButton;

    [Header("UI Text")]
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    // Activate all pause menu UI elements
    public void SetPauseUI()
    {
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        saveAndQuitButton.gameObject.SetActive(true);
    }

    // Activate all victory UI elements
    public void SetVictoryUI(int count)
    {
        winCountDown.gameObject.SetActive(true);
        winCountDown.text = count.ToString("0");
    }

    // Activate all GameOver UI elements
    public void SetGameOverUI()
    {
        deathText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        saveAndQuitButton.gameObject.SetActive(true);
    }

    // Disable all UI elements
    public void SetDisableUI()
    {
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        saveAndQuitButton.gameObject.SetActive(false);
    }
}
