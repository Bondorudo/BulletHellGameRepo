using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    private EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyManager.Rotations();
        enemyManager.Shooting();
    }

    private void FixedUpdate()
    {
        enemyManager.Movement();
    }
}
