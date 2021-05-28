using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameType { STORY, ARCADE }

public class SideScrollingGameManager : MonoBehaviour
{
    private UI_Script uiScript;
    private GameManager gm;
    private EnemySpawner enemySpawner;
    private AreAllEnemiesDead areAllEnemiesDead;

    private int enemiesKilled;
    public int enemiesToBeKilled = 20;


    private bool isBossDead;
    private bool isWaveOver;

    public GameType gameType;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        enemySpawner = GameObject.FindWithTag("GameManager").GetComponent<EnemySpawner>();
        areAllEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();
        StartCoroutine(SpawnEnemies());

        if (gameType == GameType.STORY)
        {
            uiScript.enemiesKilledText.gameObject.SetActive(true);
            uiScript.scoreText.gameObject.SetActive(false);
            enemiesKilled = 0;
            isBossDead = false;
        }
        else if (gameType == GameType.ARCADE)
        {
            uiScript.enemiesKilledText.gameObject.SetActive(false);
            uiScript.scoreText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameType == GameType.STORY)
        {
            if (enemiesKilled > enemiesToBeKilled)
            {
                enemiesKilled = enemiesToBeKilled;
            }
            uiScript.enemiesKilledText.text = "Enemies Killed " + enemiesKilled + "/" + enemiesToBeKilled;
            enemiesKilled = areAllEnemiesDead.enemiesKilled;

            if (areAllEnemiesDead.listOfEnemies.Count == 0 && enemiesKilled >= enemiesToBeKilled)
            {
                // TODO: SPAWN BOSS
                isBossDead = true;
            }

            if (isBossDead)
            {
                gm.Victory();
            }
        }
        else if (gameType == GameType.ARCADE)
        {
            gm.IncrementTimer();
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesKilled < enemiesToBeKilled)
        {
            while (areAllEnemiesDead.listOfEnemies.Count <= 0)
            {
                enemySpawner.SpawnEnemyWave();
                areAllEnemiesDead.listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("SideScrollEnemy"));
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
    }
}
