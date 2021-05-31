using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Pathfinding : MonoBehaviour
{
    public Transform player;
    NavMeshAgent nav;

    // AI Sight
    public bool playerIsInLOS = false;
    public float fieldOfViewAngle = 160f;
    public float losRadius = 45f;

    // AI Memory
    private bool aiMemorizePlayer = false;

    // Patrolling randomly between waypoints
    public Transform[] moveSpots;
    private int randomSpot;

    // Wait Time at waypoint for patrolling
    private float waitTime;
    public float startWaitTime = 1f;
    
    // Start chasing
    public float chaseRadius = 20f;
    public float rotationSpeed = 20f;

    // Stop chasing
    public float distToPlayer = 5.0f;
    private Transform stopPos;


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        stopPos = gameObject.transform;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= losRadius)
        {
            CheckLOS();
        }

        if (nav.isActiveAndEnabled)
        {
            if (playerIsInLOS == false && aiMemorizePlayer == false)
            {
                Patrol();
            }
            else if (playerIsInLOS == true)
            {
                aiMemorizePlayer = true;

                ChasePlayer();
                FacePlayer();
            }
            else if (aiMemorizePlayer == true && playerIsInLOS == false)
            {
                ChasePlayer();
            }
        }
    }

    private void CheckLOS()
    {
        Vector3 direction = player.position - transform.position;

        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction.normalized, out hit, losRadius))
            {
                if (hit.collider.tag == "Player")
                {
                    playerIsInLOS = true;
                    aiMemorizePlayer = true;
                }
                else playerIsInLOS = false;
            }
        }
    }

    private void Patrol()
    {
        nav.SetDestination(moveSpots[randomSpot].position);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 2.0f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else waitTime -= Time.deltaTime;
        }
    }

    private void ChasePlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= chaseRadius && distance > distToPlayer)
        {
            nav.SetDestination(player.position);
        }
        else if(nav.isActiveAndEnabled && distance <= distToPlayer)
        {
            nav.SetDestination(stopPos.position);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, losRadius);
    }
}
