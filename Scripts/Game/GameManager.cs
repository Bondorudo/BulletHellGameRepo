using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameTypes { SIDESCROLL, TOPDOWN, FIRSTPERSON }

public class GameManager : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject player;
    private UI_Script uiScript;

    public GameTypes gameType;

    public float score;
    private float scoreToShow;
    private float countDownTime;
    private float showTimer;

    public bool pauseGame;
    public bool isGameOver;
    public bool victory;

    public string nextLevelToLoad = "Level_2";
    public int levelToUnlock = 2;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        player.SetActive(true);
        pauseGame = false;
        isGameOver = false;
        victory = false;
        score = 0;
        scoreToShow = 0;
        countDownTime = 3;
        StartCoroutine(DecreaseTimer());
    }

    void Update()
    {
        PauseGame();
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


    // Decreases next level countdown
    IEnumerator DecreaseTimer()
    {
        while (true)
        {
            while (victory && countDownTime >= 0)
            {
                showTimer = (int)Math.Round(countDownTime);
                uiScript.SetVictoryUI((int)showTimer);
                countDownTime--;
                yield return new WaitForSecondsRealtime(0.8f);
            }
            LoadNextLevel();
            yield return null;
        }
    }


    // Loads the next scene by id in build settings
    public void LoadNextLevel()
    {
        if (countDownTime <= 0)
        {
            int levelReached = PlayerPrefs.GetInt("levelReached");
            if (levelReached <= levelToUnlock)
            {
                PlayerPrefs.SetInt("levelReached", levelToUnlock);
            }
            FindObjectOfType<SceneFader>().FadeTo(nextLevelToLoad);
        }
    }
}
