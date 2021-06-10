using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameTypes { SIDESCROLL, TOPDOWN, FIRSTPERSON, UNDYNE }

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
    private float lerpDuration = 0.8f;
    private float levelTextDuration = 1.5f;

    public bool pauseGame;
    public bool isGameOver;
    public bool victory;

    public string nextLevelToLoad = "Level_2";
    public int levelToUnlock = 2;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        if (gameType == GameTypes.FIRSTPERSON || gameType == GameTypes.SIDESCROLL)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        player.SetActive(true);
        pauseGame = true;
        isGameOver = false;
        victory = false;
        score = 0;
        scoreToShow = 0;
        countDownTime = 3f;
        uiScript.levelNumberText.gameObject.SetActive(true);
        uiScript.levelNumberText.text = (levelToUnlock - 1).ToString();

        StartCoroutine(DecreaseTimer());
        StartCoroutine(ShowLevelNumber());
    }

    void Update()
    {
        PauseGame();
    }

    IEnumerator ShowLevelNumber()
    {
        yield return new WaitForSecondsRealtime(levelTextDuration);

        float endValue = 0;
        float elapsedTime = 0;
        float startValue = uiScript.levelNumberText.color.a;

        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / lerpDuration);
            uiScript.levelNumberText.color = new Color(uiScript.levelNumberText.color.r, uiScript.levelNumberText.color.g, uiScript.levelNumberText.color.b, newAlpha);
            yield return null;
        }
        uiScript.levelNumberText.gameObject.SetActive(false);
        pauseGame = false;
    }

    public void PauseGame()
    {
        if (!isGameOver || !victory)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) && !pauseGame)
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
            if (gameType == GameTypes.SIDESCROLL)
            {

                uiScript.scoreText.text = "Score " + scoreToShow;
            }
            else if (gameType == GameTypes.UNDYNE)
            {
                uiScript.scoreText.text = "Score" + "\n" + scoreToShow;
            }
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
                yield return new WaitForSecondsRealtime(0.7f);
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
