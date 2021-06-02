using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed;
    [HideInInspector] public int damageToGive;

    public float iFrameCounter;
    private float iFrames = 0f;

    private Vector2 moveDirection;
    [SerializeField] private Vector3 shootDir;

    private GameManager gm;
    private GameTypes gameType;

    private void OnEnable()
    {
        Invoke("DisableBullet", 10f);
    }

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        gameType = gm.gameType;
    }

    void Update()
    {
        if (gameType == GameTypes.SIDESCROLL)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        else if (gameType == GameTypes.TOPDOWN || gameType == GameTypes.FIRSTPERSON)
        {
            transform.position += shootDir * speed * Time.deltaTime;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("MovingWall"))
        {
            Destroy(gameObject);
        }
        // If Bullet collides with player destroy bullet and damage player
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
            AudioManager.instance.PlaySound("BulletCollision");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public void SetUp(Vector3 shootDir)
    {
        this.shootDir = shootDir;
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
