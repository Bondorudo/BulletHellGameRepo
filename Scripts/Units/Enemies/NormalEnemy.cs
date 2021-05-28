using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    private GameObject thePlayer;
    private GunController gunController;

    [SerializeField] private float speed;
    [SerializeField] private float damping = 5;
    [SerializeField] private float coolDownDefault = 0.1f;
    private float coolDown = 0;
    private bool canShoot;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thePlayer = GameObject.FindWithTag("Player");
        gunController = GetComponent<GunController>();
        StartCoroutine(ShootEnum());
    }

    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;
        RotateTowardPlayer();
        Shooting();
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

    public void Shooting()
    {
        if (canShoot == true && IsTouchingWall() == false && coolDown >= coolDownDefault)
        {
            gunController.Shoot();
            audioManager.EnemyProjectileAudio();
            coolDown = 0;
        }
    }

    // Start of game dont allow enemies to shoot until 0.5 seconds have passed
    IEnumerator ShootEnum()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
