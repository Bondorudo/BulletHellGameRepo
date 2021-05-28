using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBulletController : MonoBehaviour
{
    public float speed;
    [HideInInspector] public int damageToGive;

    public float iFrameCounter;
    private float iFrames = 0f;

    private AudioManager audioManager;

    private Vector2 moveDirection;

    public GameTypes gameType;

    private void OnEnable()
    {
        Invoke("DisableBullet", 10f);
    }

    private void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (gameType == GameTypes.SIDESCROLL)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        else if (gameType == GameTypes.TOPDOWN || gameType == GameTypes.FIRSTPERSON)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (iFrameCounter >= iFrames)
        {
            iFrameCounter -= Time.deltaTime;
        }
    }

    // Enemy Bullets
    private void OnCollisionEnter(Collision collision)
    {
        // If Bullet hits walls destroy it
        if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
        // If Purple collides with player destroy purple and damage player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet" && gameObject.tag == "Orange" && iFrameCounter >= iFrames)
        {
            Destroy(gameObject);
        }

        // If Player Bullet and Orange Enemy Bullet collide destroy both bullets
        if (other.gameObject.tag == "PlayerBullet" && gameObject.tag == "Orange" && iFrameCounter <= iFrames)
        {
            audioManager.BulletCollisionAudio();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void DisableBullet()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
