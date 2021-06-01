using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Pathfinding : MonoBehaviour
{
    [Header("Transform")]
    public Transform player;
    NavMeshAgent nav;

    // AI Sight
    [Header("Line Of Sight")]
    public bool playerIsInLOS = false;
    public float fieldOfViewAngle = 160f;
    public float losRadius = 45f;

    // AI Memory
    [Header("Memory")]
    private bool aiMemorizePlayer = false;

    // Start chasing
    [Header("Start Chasing")]
    public float chaseRadius = 20f;
    public float rotationSpeed = 20f;

    // Stop chasing
    [Header("Stop Chasing")]
    public float distToPlayer = 5.0f;
    private Transform stopPos;


    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = true;
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
            if (playerIsInLOS == true)
            {
                aiMemorizePlayer = true;

                ChasePlayer();
                FacePlayer();
            }
            else if (aiMemorizePlayer == true && playerIsInLOS == false)
            {
                ChasePlayer();
                FacePlayer();
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
