using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    private GunController gunController;

    [SerializeField] private bool rotateClockwise;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float coolDownDefault = 0.1f;
    private float coolDown = 0;
    private bool canShoot;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunController = GetComponent<GunController>();
        StartCoroutine(ShootEnum());
    }

    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;
        Shooting();
        ContinuousRotation();
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

    public void ContinuousRotation()
    {
        if (rotateClockwise == true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (rotateClockwise == false)
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
    }
}
