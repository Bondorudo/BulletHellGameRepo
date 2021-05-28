using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopFirstPersonGameManager : MonoBehaviour
{
    private LevelHeart levelHeart;
    private AreAllEnemiesDead areEnemiesDead;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        levelHeart = GameObject.FindWithTag("WinCondition").GetComponent<LevelHeart>();
        areEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanHeartTakeDamage();
        CheckVictory();
    }

    public void CheckCanHeartTakeDamage()
    {
        if (areEnemiesDead.AreTheyDestroyed())
        {
            levelHeart.CanTakeDamage();
            levelHeart.SetVulnerableColor();
        }
    }

    public void CheckVictory()
    {
        int enemyHealth = levelHeart.CurrentHealth();
        if (enemyHealth == 0)
        {
            gm.Victory();
        }
    }
}
