using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyneEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject enemyPrefab;
    private AreAllEnemiesDead areAllEnemiesDead;

    int enemyIndex;

    [SerializeField] private float timeBeforeActivationSet;

    //TODO: Spawn pos lista jota järjestelemällä voi muuttaa spawn järjestystä
    [Range(0,3)]
    [SerializeField] private int spawnPos = 0;
    [Range(0, 4)]
    public int amountOfEnemies = 0;
    Vector3[] spawnPosArr = new Vector3[4];

    private void Awake()
    {
        // Initialize possible enemy positions with arrays depending on how many enemies are spawned
        spawnPosArr[0] = new Vector3(0f, 1f, 6f);
        spawnPosArr[1] = new Vector3(0f, 1f, -6f);
        spawnPosArr[2] = new Vector3(6f, 1f, 0f);
        spawnPosArr[3] = new Vector3(-6f, 1f, 0f);
        areAllEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    public void SpawnEnemyWave()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPosArr[spawnPos], enemyPrefab.transform.rotation) as GameObject;

        // Add enemy to list
        areAllEnemiesDead.listOfEnemies.Add(enemy);

        spawnPos++;
        if (spawnPos >= 4)
        {
            spawnPos = 0;
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
