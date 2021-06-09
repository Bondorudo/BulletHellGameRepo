using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUndyne : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    Vector3 direction;

    [SerializeField] private float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        direction = new Vector3(0f, transform.position.y - player.position.y, 0f);
        transform.LookAt(direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * moveSpeed * Time.deltaTime;
    }
}
