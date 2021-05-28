using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem explosionParticle;
    [HideInInspector] public AudioManager audioManager;
    private AreAllEnemiesDead areAllEnemiesDead;

    public int maxHealth;
    private int currentHealth;

    public bool canTakeDamage;
    private bool isTouchingWall;

    private void Awake()
    {
        canTakeDamage = true;
        isTouchingWall = false;
        currentHealth = maxHealth;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        areAllEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    public void HurtEnemy(int damage)
    {
        if (canTakeDamage == true)
        {
            audioManager.EnemyDamageAudio();
            currentHealth -= damage;
            EnemyDeath();
        }
    }

    public bool EnemyDeath()
    {
        //Destroy enemy object when its health is 0
        if (currentHealth <= 0)
        {
            audioManager.EnemyDeathAudio();
            explosionParticle.transform.parent = null;
            explosionParticle.Play();
            areAllEnemiesDead.DestroyedCondition(gameObject);
            Destroy(gameObject);
            return true;
        }
        else return false;
    }

    public int CurrentHealth()
    {
        return currentHealth;
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

    public bool IsTouchingWall()
    {
        if (isTouchingWall)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            HurtEnemy(other.gameObject.GetComponent<PlayerBulletController>().damageToGive);
        }
    }
}
