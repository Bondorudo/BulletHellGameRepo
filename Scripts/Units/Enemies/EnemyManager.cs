using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject thePlayer;
    private AreAllEnemiesDead areEnemiseDead;
    private GameManager gm;
    private GunController gunController;
    public ParticleSystem explosionParticle;
    private AudioManager audioManager;

    public float speed = 3;
    public float damping = 5;
    public float rotationSpeed = 10f;

    public float coolDownDefault = 0.1f;
    private float coolDown = 0;

    public int health = 3;
    private int currentHealth;

    public bool canTakeDamage = true;
    public bool rotateClockwise = true;
    private bool isTouchingWall = false;
    private bool canShoot;


    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        enemyRb = GetComponent<Rigidbody>();
        thePlayer = GameObject.FindWithTag("Player");
        gunController = GetComponent<GunController>();
        areEnemiseDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        currentHealth = health;
        StartCoroutine(ShootEnum());
        canShoot = false;
    }

    private void Update()
    {
        coolDown += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isTouchingWall = false;
        }
    }

    public void Shooting()
    {
        if (canShoot == true && isTouchingWall == false && coolDown >= coolDownDefault)
        {
            gunController.Shoot();
            audioManager.EnemyProjectileAudio();
            coolDown = 0;
        }
    }

    IEnumerator ShootEnum()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    public void Movement()
    {
        enemyRb.velocity = (transform.forward * speed);
    }

    public void Rotations()
    {
        if (gm.isGameOver == false)
        {
            //Enemy rotates to look at the player
            var rotation = Quaternion.LookRotation(thePlayer.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }
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

    //Enemy takes damage
    public void HurtEnemy(int damage)
    {
        if (canTakeDamage == true)
        {
            audioManager.EnemyDamageAudio();
            currentHealth -= damage;
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        //Destroy enemy object when its health is 0
        if (currentHealth <= 0)
        {
            audioManager.EnemyDeathAudio();

            explosionParticle.transform.parent = null;
            explosionParticle.Play();
            areEnemiseDead.DestroyedCondition(gameObject);
            gameObject.SetActive(false);
        }
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }
}
