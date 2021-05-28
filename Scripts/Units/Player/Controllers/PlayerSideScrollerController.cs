using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideScrollerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float moveSpeed;


    void Start()
    {
        // Get a reference to components
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Restricts the players x movement to stay in bounds of the player area
        if (transform.position.x < -12)
        {
            transform.position = new Vector3(-12, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > -2)
        {
            transform.position = new Vector3(-2, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        // Apply velocity to player rigidBody
        rb.velocity = PlayerMovement();
    }

    public Vector3 PlayerMovement()
    {
        // Get input and get new velocity with input * speed * time
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed * Time.deltaTime;
        return moveVelocity;
    }
}
