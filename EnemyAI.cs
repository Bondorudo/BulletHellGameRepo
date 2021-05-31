using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent enemy;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private float sightrange;
    private bool playerInSightRange;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightrange, whatIsPlayer);

        if (playerInSightRange) enemy.SetDestination(player.position);
        if (!playerInSightRange) enemy.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
}
