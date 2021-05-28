using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideScroll : Enemy
{
    private FireBullets fireBullets;

    [SerializeField] private float leftWall = 7;
    [SerializeField] private int moveSpeed;
    private bool hasEnteredFight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fireBullets = GetComponent<FireBullets>();
        StartCoroutine(Shooting());
    }


    private void FixedUpdate()
    {
        if (transform.position.x >= leftWall)
        {
            hasEnteredFight = false;
            Movement();
        }
        else
        {
            hasEnteredFight = true;
            StopMovement();
        }
    }

    private void Movement()
    {
        rb.velocity = (transform.forward * moveSpeed);
    }

    private void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    IEnumerator Shooting()
    {
        while (this.isActiveAndEnabled)
        {
            while (hasEnteredFight == true)
            {
                fireBullets.Fire();
                yield return new WaitForSeconds(fireBullets.fireRate);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
