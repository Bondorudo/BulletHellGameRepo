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
    public Button saveAndQuitButton;

    [Header("UI Text")]
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI scoreText;


    public void SetPauseUI()
    {
        pauseText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        saveAndQuitButton.gameObject.SetActive(true);
    }

    public void SetVictoryUI(int count)
    {
        winCountDown.gameObject.SetActive(true);
        winCountDown.text = count.ToString("0");
    }

    public void SetGameOverUI()
    {
        deathText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        saveAndQuitButton.gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        saveAndQuitButton.gameObject.SetActive(false);
    }
}
