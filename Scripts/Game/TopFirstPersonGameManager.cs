using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TopFirstPersonGameManager : MonoBehaviour
{
    private LevelHeart levelHeart;
    private AreAllEnemiesDead deadEnemies;
    private GameManager gm;

    private bool checkIfCanTakeDamage;


    void Start()
    {
        checkIfCanTakeDamage = true;
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        levelHeart = GameObject.FindWithTag("LevelHeart").GetComponent<LevelHeart>();
        deadEnemies = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
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
            gm.victory = true;
            gm.Victory();
        }
    }
}
