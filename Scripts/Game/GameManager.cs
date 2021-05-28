using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum GameTypes { SIDESCROLL, TOPDOWN, FIRSTPERSON }

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private AudioManager audioManager;
    private UI_Script uiScript;

    private float score;
    private float scoreToShow;

    public bool pauseGame;
    public bool isGameOver;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.SetActive(true);
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        pauseGame = false;
        isGameOver = false;
        score = 0;
        scoreToShow = 0;
    }

    void Update()
    {
        PauseGame();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player.GetComponent<CurrentPlayerData>());
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        player.GetComponent<PlayerHealthManager>().currentHealth = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;
    }

    public void PauseGame()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                pauseGame = true;
                uiScript.PauseMenu();
            }
        }
        if (pauseGame == true)
        {
            Time.timeScale = 0;
        }
        else if (pauseGame == false)
        {
            Time.timeScale = 1;
        }
    }

    public void IncrementTimer()
    {
        if (pauseGame == false)
        {
            score += Time.deltaTime;
            scoreToShow = (float)Math.Round(score, 2);
            uiScript.scoreText.text = "Score " + scoreToShow;
        }
    }

    public void PauseMenu()
    {
        pauseGame = true;
        uiScript.PauseMenu();
    }

    public void Victory()
    {
        pauseGame = true;
        isGameOver = true;
        uiScript.Victory();
    }

    public void GameOver()
    {
        player.SetActive(false);
        isGameOver = true;
        pauseGame = true;
        uiScript.GameOver();
    }

    public void NextLevelButton()
    {
        audioManager.ButtonPressAudio();
        uiScript.NextLevelButton();
    }

    public void ContinueButton()
    {
        audioManager.ButtonPressAudio();
        pauseGame = false;
        uiScript.ContinueButton();
    }
    public void RestartButton()
    {
        audioManager.ButtonPressAudio();
        uiScript.RestartButton();
    }

    public void QuitToMenu()
    {
        uiScript.QuitToMenu();
    }
}
