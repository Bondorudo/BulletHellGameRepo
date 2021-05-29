using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TopFirstPersonGameManager : MonoBehaviour
{
    private LevelHeart levelHeart;
    private AreAllEnemiesDead deadEnemies;
    private GameManager gm;
    private UI_Script uiScript;

    private bool checkIfCanTakeDamage;
    private bool victory;

    private int nextLevelToLoad;
    private int sceneCount;

    private float countDownTime;
    private float showTimer;


    void Start()
    {
        countDownTime = 3;
        victory = false;
        checkIfCanTakeDamage = true;
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        levelHeart = GameObject.FindWithTag("LevelHeart").GetComponent<LevelHeart>();
        deadEnemies = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
        nextLevelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        sceneCount = SceneManager.sceneCountInBuildSettings;
        StartCoroutine(DecreaseTimer());
    }

    void Update()
    {
        CheckCanHeartTakeDamage();
        CheckIfHeartsAreDead();
    }

    public void CheckCanHeartTakeDamage()
    {
        if (deadEnemies.AreEnemiesDead() && checkIfCanTakeDamage)
        {
            checkIfCanTakeDamage = false;
            levelHeart.CanTakeDamage();
            levelHeart.SetVulnerableColor();
        }
    }

    public void CheckIfHeartsAreDead()
    {
        if (deadEnemies.AreHeartsDead())
        {
            victory = true;
            gm.Victory();
        }
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
                yield return new WaitForSecondsRealtime(1f);
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
            if (nextLevelToLoad < sceneCount)
            {
                SceneManager.LoadScene(nextLevelToLoad);
            }
            // Bring to main menu if next level cant be loaded
            // TODO: Add end credits here
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
