using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameManager gm;

    [Header("Game Objects")]
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform firePoint;

    [Header("Bullet Attributes")]
    [SerializeField] private float bulletCooldownDefault = 0.1f;
    private float bulletCooldown = 0;
    [SerializeField] private float bulletSpeed = 25;
    [SerializeField] private int bulletDamage = 1;


    void Start()
    {
        // Get a reference to components
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Increase shooting coolDown
        bulletCooldown += Time.deltaTime;

        // Call shoot function when left click is pressed or held
        if (Input.GetMouseButton(0) && !gm.pauseGame)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (bulletCooldown > bulletCooldownDefault)
        {
            // Create a player bullet as a playerBulletController then set bullet attributes, play audio and reset cooldown
            Vector3 shootDir = (firePoint.position - this.gameObject.transform.position).normalized;
            PlayerBulletController bullet = Instantiate(playerBullet, firePoint.position, firePoint.rotation).GetComponent<PlayerBulletController>();
            bullet.SetUp(shootDir);
            bullet.bulletSpeed = bulletSpeed;
            bullet.damageToGive = bulletDamage;
            AudioManager.instance.PlaySound("PlayerBullet");
            bulletCooldown = 0;
        }
    }
}
