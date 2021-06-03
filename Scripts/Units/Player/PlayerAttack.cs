using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameManager gm;

    [Header("Game Objects")]
    [SerializeField] private PlayerBulletController bullet;
    [SerializeField] private Transform firePoint;

    [Header("Bullet Attributes")]
    [SerializeField] private float timeBetweenBullets = 0.1f;
    private float bulletCooldown = 0;
    [SerializeField] private float bulletSpeed = 25;
    [SerializeField] private int bulletDamage = 1;


    void Start()
    {
        bulletCooldown = timeBetweenBullets;
        // Get a reference to components
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Increase shooting coolDown
        if (bulletCooldown > 0) 
        {
            bulletCooldown -= Time.deltaTime;
        }

        // can shoot if game isnt paused
        if (!gm.pauseGame)
        {
            // Call shoot function when left click is pressed or held
            if (Input.GetMouseButton(0) && bulletCooldown <= 0)
            {
                Shoot();
                bulletCooldown = timeBetweenBullets;
                AudioManager.instance.PlaySound("PlayerBullet");
            }
        }
    }

    public void Shoot()
    {
        // Create a player bullet as a playerBulletController then set bullet attributes, play audio and reset cooldown
        PlayerBulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as PlayerBulletController;
        newBullet.bulletSpeed = bulletSpeed;
        newBullet.damageToGive = bulletDamage;
    }
}
