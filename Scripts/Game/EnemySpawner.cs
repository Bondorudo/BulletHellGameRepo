using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int[][] enemyYposArray = new int[3][];

    public List<GameObject> enemyPrefabs = new List<GameObject>();
    
    int yPosIndex;
    int yPos;
    int enemyIndex;
    int enemiesInWave;


    private void Start()
    {
        // Initialize possible enemy Y positions with arrays depending on how many enemies are spawned
        enemyYposArray[0] = new int[] { 0 };
        enemyYposArray[1] = new int[] { -2, 2 };
        enemyYposArray[2] = new int[] { -3, 0, 3 };
    }

    public void SpawnEnemyWave()
    {
        yPosIndex = 0;
        enemiesInWave = Random.Range(1, 4);

        // Loop trought enemies in wave and spawn an enemy for every enemy in wave
        for (int i = 0; i < enemiesInWave; i++)
        {
            // Set y position for spawned enemies based on array y positions;
            yPos = enemyYposArray[enemiesInWave - 1][yPosIndex];

            // new Vector3 for spawn position
            Vector3 spawn = new Vector3(15, yPos, 0);
            Instantiate(RandomEnemy(), spawn, RandomEnemy().transform.rotation);

            yPosIndex++;
        }
    }

    // Gets random enemy from list of enemeis
    public GameObject RandomEnemy()
    {
        enemyIndex = Random.Range(0, 8);
        GameObject randomEnemy = enemyPrefabs[enemyIndex];

        return randomEnemy;
    }
}
