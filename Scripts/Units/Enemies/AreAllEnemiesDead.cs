using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreAllEnemiesDead : MonoBehaviour
{
    public List<GameObject> listOfEnemies = new List<GameObject>();
    public List<GameObject> listOfHearts = new List<GameObject>();
    public int enemiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("BreakableWall"));
        listOfHearts.AddRange(GameObject.FindGameObjectsWithTag("LevelHeart"));
    }

    public void DestroyedCondition(GameObject gameObject)
    {
        if (listOfEnemies.Contains(gameObject))
        {
            listOfEnemies.Remove(gameObject);
            enemiesKilled++;
        }
        if (listOfHearts.Contains(gameObject))
        {
            listOfHearts.Remove(gameObject);
            enemiesKilled++;
        }
    }

    public bool AreEnemiesDead()
    {
        if (listOfEnemies.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool AreHeartsDead()
    {
        if (listOfHearts.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
