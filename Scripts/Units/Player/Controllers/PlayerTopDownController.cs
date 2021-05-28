using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    [SerializeField] private ParticleSystem movementParticles;
    [SerializeField] private float moveSpeed;

    private Vector3 moveInput;
    private Vector3 moveVelocity;


    void Start()
    {
        // Get a reference to components
        mainCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        TopDownRotation();

        // Play or Stop movement particles if player is moving or not
        if (rb.velocity != Vector3.zero)
        {
            if (!movementParticles.isPlaying) movementParticles.Play();
        }
        else
        {
            if (movementParticles.isPlaying) movementParticles.Stop();
        }
    }

    private void FixedUpdate()
    {
        // Apply velocity to player rigidbody
        rb.velocity = moveVelocity;
    }

    public void Movement()
    {
        // Velocity is equal to movement input * speed;
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    // Rotate player to point players cursor
    public void TopDownRotation()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.Rotate(0f, 45f, 0f, Space.World);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
