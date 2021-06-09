using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUndyne : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    private EnemyGun gun;
    Vector3 direction;

    [SerializeField] private float moveSpeed = 1f;
    public float timeBeforeActivationSet;
    private float timeBeforeActivation;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<EnemyGun>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        timeBeforeActivation = 0;
        direction = new Vector3(0f, transform.position.y - player.position.y, 0f);
        transform.LookAt(direction);
        gun.canShoot = false;
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeActivation > timeBeforeActivationSet)
        {
            gun.canShoot = true;
            canMove = true;
        }
        else
        {
            timeBeforeActivation += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
