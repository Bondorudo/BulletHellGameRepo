using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    public GameObject enemyBody;
    private AreAllEnemiesDead areAllEnemiesDead;

    private void Awake()
    {
        areAllEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBody == null)
        {
            Destroy(gameObject);
            areAllEnemiesDead.DestroyedCondition(gameObject);
        }
    }
}
