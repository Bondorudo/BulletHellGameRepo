using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : Enemy
{
    private GameObject thePlayer;

    [SerializeField] private float speed;
    [SerializeField] private float damping = 5;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thePlayer = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardPlayer();
    }

    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * speed);
    }

    public void RotateTowardPlayer()
    {
        //Enemy rotates to look at the player
        var rotation = Quaternion.LookRotation(thePlayer.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
