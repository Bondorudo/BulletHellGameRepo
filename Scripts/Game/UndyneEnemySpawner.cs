using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyneEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject enemyPrefab;

    int enemyIndex;
    int multiplier = 0;

    [SerializeField] private float timeBeforeActivationSet;

    [Range(0,4)]
    [SerializeField] private int spawnPos = 0;

    Vector3[] spawnPosArr = new Vector3[4];

    private void Awake()
    {
        multiplier = 0;
        // Initialize possible enemy positions with arrays depending on how many enemies are spawned
        spawnPosArr[0] = new Vector3(0f, 1f, 6f);
        spawnPosArr[1] = new Vector3(6f, 1f, 0f);
        spawnPosArr[2] = new Vector3(0f, 1f, -6f);
        spawnPosArr[3] = new Vector3(-6f, 1f, 0f);
    }

    public void SpawnEnemyWave()
    {
        // Loop trought enemies in wave and spawn an enemy for every enemy in wave
        for (int i = 0; i < 4; i++)
        {
            multiplier++;
            GameObject enemy = Instantiate(enemyPrefab, spawnPosArr[spawnPos], enemyPrefab.transform.rotation) as GameObject;
            enemy.GetComponent<EnemyUndyne>().timeBeforeActivationSet += multiplier * (int)1.25f;
            spawnPos++;
            if (spawnPos >= 4)
            {
                spawnPos = 0;
            }
        }
    }

    // Gets random enemy from list of enemeis
    public GameObject RandomEnemy()
    {
        enemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject randomEnemy = enemyPrefabs[enemyIndex];

        return randomEnemy;
    }
}
