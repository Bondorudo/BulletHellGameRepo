using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyneEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject enemyPrefab;

    int enemyIndex;
    int enemiesInWave;

    int thisNumber;

    int[] randomNumbers = new int[4];

    Vector3[] spawnPosArr = new Vector3[3];

    private void Start()
    {
        // Initialize possible enemy positions with arrays depending on how many enemies are spawned

        spawnPosArr[0] = new Vector3(0, 1, 6);
        spawnPosArr[1] = new Vector3(6, 1, 0);
        spawnPosArr[2] = new Vector3(0, 1, -6);
        spawnPosArr[3] = new Vector3(-6, 1, 0);
    }

    public void SpawnEnemyWave()
    {
        /*
        List<int> numbers = new List<int>(4);
        for (int i = 0; i < 121; i++)
        {
            numbers.Add(i);
        }
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            thisNumber = Random.Range(0, numbers.Count);
            randomNumbers[i] = numbers[thisNumber];
            numbers.RemoveAt(thisNumber);
        }
        */
        enemiesInWave = Random.Range(1, 5);

        // Loop trought enemies in wave and spawn an enemy for every enemy in wave
        for (int i = 0; i <= 4; i++)
        {
            // new random Vector3 for spawn position
            int randomIndex = Random.Range(0, 4);

            Instantiate(enemyPrefab, spawnPosArr[randomIndex], enemyPrefab.transform.rotation);
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
