using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UndyneGameManager : MonoBehaviour
{
    public GameType gameType;

    private UI_Script uiScript;
    private GameManager gm;
    private UndyneEnemySpawner enemySpawner;
    private AreAllEnemiesDead areAllEnemiesDead;

    private int enemiesKilled;
    public int enemiesToBeKilled = 20;

    private bool isBossDead;
    bool canSpawnWave;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        enemySpawner = GameObject.FindWithTag("GameManager").GetComponent<UndyneEnemySpawner>();
        areAllEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();

        // Set up level and UI based on if scene is in story or arcade mode;
        if (gameType == GameType.STORY)
        {
            uiScript.scoreText.gameObject.SetActive(false);
            enemiesKilled = 0;
            isBossDead = false;
        }
        else if (gameType == GameType.ARCADE)
        {
            uiScript.scoreText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Story mode
        if (gameType == GameType.STORY)
        {
            // If enemies killed is higher than win condition set it to win condition
            if (enemiesKilled > enemiesToBeKilled)
            {
                enemiesKilled = enemiesToBeKilled;
            }

            // Set enemies killed to amount of enemies killed
            enemiesKilled = areAllEnemiesDead.enemiesKilled;

            // Once all enemies in level are dead spawn a boss
            if (areAllEnemiesDead.listOfEnemies.Count == 0 && enemiesKilled >= enemiesToBeKilled)
            {
                // TODO: SPAWN BOSS
                isBossDead = true;
            }

            // Win level after boss is dead
            if (isBossDead)
            {
                gm.Victory();
            }
        }
        // Arcade mode
        else if (gameType == GameType.ARCADE)
        {
            // Increase player score
            gm.IncrementTimer();
            if (gm.isGameOver)
            {
                uiScript.highScoreText.gameObject.SetActive(true);
                float highScore = PlayerPrefs.GetFloat("undyneHighScore");
                if (highScore <= gm.score)
                {
                    PlayerPrefs.SetFloat("undyneHighScore", gm.score);
                }
                float scoreToShow = (float)Math.Round(highScore, 2);
                uiScript.highScoreText.text = "High Score " + scoreToShow;
            }
        }
        SpawnEnemies();
    }

    // Spawns enemies everytime a wave has been killed until enemy limit has been reached
    private void SpawnEnemies()
    {

        // Do this while enemies killed is less than win condition
        if (enemiesKilled < enemiesToBeKilled)
        {
            // Spawn enemies when there are no enemies in the scene
            if (areAllEnemiesDead.listOfEnemies.Count <= 0)
            {
                canSpawnWave = true;
                if (canSpawnWave)
                {
                    // Spawn a wave of enemies
                    StartCoroutine(enemySpawner.SpawnEnemyWave());
                    
                    canSpawnWave = false;
                }
                
                // Add all objects with SideScrollEnemy tag to list;
                areAllEnemiesDead.listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("UndyneEnemy"));
            }
            if (areAllEnemiesDead.listOfEnemies.Count >= enemySpawner.amountOfEnemies)
            {
                StopCoroutine(enemySpawner.SpawnEnemyWave());
            }
        }
    }
}
