using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopFirstPersonGameManager : MonoBehaviour
{
    private LevelHeart levelHeart;
    private AreAllEnemiesDead areEnemiesDead;
    private GameManager gm;

    private bool checkIfCanTakeDamage;
    private bool victory;

    // Start is called before the first frame update
    void Start()
    {
        checkIfCanTakeDamage = true;
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        levelHeart = GameObject.FindWithTag("WinCondition").GetComponent<LevelHeart>();
        areEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanHeartTakeDamage();
        CheckVictory();
        Victory();
    }

    public void CheckCanHeartTakeDamage()
    {
        if (areEnemiesDead.AreTheyDestroyed() && checkIfCanTakeDamage)
        {
            checkIfCanTakeDamage = false;
            levelHeart.CanTakeDamage();
            levelHeart.SetVulnerableColor();
        }
    }

    public void CheckVictory()
    {
        int heartHealth = levelHeart.CurrentHealth();
        if (heartHealth <= 0)
        {
            victory = true;
            gm.Victory();
        }
    }

    public bool Victory()
    {
        if (victory)
        {
            return true;
        }
        else return false;
    }
}
