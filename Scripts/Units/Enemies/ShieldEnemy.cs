using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    private GameObject target;
    private Rigidbody rb;

    public GameObject enemyBody;

    [SerializeField] private float startSpeed;
    private float speed;
    [SerializeField] private float damping = 5;


    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBody == null)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //MoveTowardPlayer();
    }

    public void MoveTowardPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(target.transform);
    }
}
