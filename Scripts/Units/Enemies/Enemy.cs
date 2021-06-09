using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem explosionParticle;
    [HideInInspector] public Rigidbody rb;
    private AreAllEnemiesDead areAllEnemiesDead;

    [Header("Health")]
    public int maxHealth;
    private int currentHealth;

    [Header("Can Take Damage")]
    [HideInInspector]public bool canTakeDamage;
    private bool isTouchingWall;

    private void Awake()
    {
        canTakeDamage = true;
        isTouchingWall = false;
        currentHealth = maxHealth;
        areAllEnemiesDead = GameObject.FindWithTag("GameManager").GetComponent<AreAllEnemiesDead>();
    }

    public void HurtEnemy(int damage)
    {
        if (canTakeDamage == true)
        {
            AudioManager.instance.PlaySound("EnemyDamage");
            currentHealth -= damage;
            EnemyDeath();
        }
    }

    public bool EnemyDeath()
    {
        //Destroy enemy object when its health is 0
        if (currentHealth <= 0)
        {
            areAllEnemiesDead.DestroyedCondition(gameObject);
            AudioManager.instance.PlaySound("EnemyDeath");
            explosionParticle.transform.parent = null;
            explosionParticle.Play();
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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
        if (other.gameObject.tag == "PlayerBullet" && canTakeDamage)
        {
            Destroy(other.gameObject);
            HurtEnemy(other.gameObject.GetComponent<PlayerBulletController>().damageToGive);
        }

        if (other.gameObject.tag == "PlayerBullet" && !canTakeDamage)
        {
            Destroy(other.gameObject);
            AudioManager.instance.PlaySound("ShieldedEnemyHit");
        }
    }
}
