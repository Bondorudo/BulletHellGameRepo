using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstPersonController : MonoBehaviour
{
    private PlayerHealthManager healthManager;
    private Rigidbody rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;

    private Vector3 move;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed;

        
        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);

        rb.velocity = newMovePos;
    }
}
