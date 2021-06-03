using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TopFirstPersonGameManager : MonoBehaviour
{
    private AreAllEnemiesDead deadEnemies;
    private GameManager gm;

    // Boolean to do CheckCanHeartTakeDamage only once when it can be done;
    private bool checkIfCanTakeDamage;


    void Start()
    {
        checkIfCanTakeDamage = true;
        // Get a reference to components
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        deadEnemies = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    void Update()
    {
        CheckCanHeartTakeDamage();
        CheckIfHeartsAreDead();
    }

    // Checks if all level enemies are dead if they are allow player to damage levelHearts
    public void CheckCanHeartTakeDamage()
    {
        if (deadEnemies.AreEnemiesDead() && checkIfCanTakeDamage)
        {
            checkIfCanTakeDamage = false;
            foreach(LevelHeart heart in FindObjectsOfType<LevelHeart>())
            {
                heart.CanTakeDamage();
                heart.SetVulnerableColor();
            }
        }
    }

    // Checks if all level hearts are dead if they are win the level
    public void CheckIfHeartsAreDead()
    {
        if (deadEnemies.AreHeartsDead())
        {
            gm.Victory();
        }
    }
}
