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
    private Camera mainCamera;
    private GameObject player;
    private UI_Script uiScript;

    private float score;
    private float scoreToShow;

    public GameTypes gameType;

    public bool pauseGame;
    public bool isGameOver;
    public bool victory;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
        player.SetActive(true);
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        pauseGame = false;
        isGameOver = false;
        victory = false;
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
        if (!isGameOver || !victory)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                pauseGame = true;
                uiScript.SetPauseUI();
                Cursor.lockState = CursorLockMode.None;
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

    public void Victory()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseGame = true;
        victory = true;
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        mainCamera.transform.parent = null;
        player.SetActive(false);
        isGameOver = true;
        pauseGame = true;
        uiScript.SetGameOverUI();
    }
}
