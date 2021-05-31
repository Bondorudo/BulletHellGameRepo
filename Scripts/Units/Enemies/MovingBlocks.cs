using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { HORIZONTAL, VERTICAL}

public class MovingBlocks : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Direction direction;

    [SerializeField] private float speed;
    private Vector3 movementDirection;

    private bool test;


    void Start()
    {
        test = true;
        rb = GetComponent<Rigidbody>();

        // Horizontal movement
        if (direction == Direction.HORIZONTAL)
        {
            movementDirection = transform.right;
        }
        // Vertical movement
        if (direction == Direction.VERTICAL)
        {
            movementDirection = transform.forward;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("MovingWall") && test == true)
        {
            speed *= -1;
            test = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("MovingWall"))
        {
            test = true;
        }
    }
}
