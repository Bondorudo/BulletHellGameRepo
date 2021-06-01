using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { NONBREAKABLE, BREAKABLE, ALTERNATE }

public class FireBullets : MonoBehaviour
{
    [Header("FIREPOINTS")]
    [Range(1, 4)]
    [SerializeField] private int firePointCount;
    public Transform firePoint;

    [Header("ANGLE")]
    [SerializeField] private float startAngle = 135f;
    [SerializeField] private float endAngleA = 225f;

    [Header("BULLETS")]
    [SerializeField] private GameObject BulletBreakable;
    [SerializeField] private GameObject BulletNonBreakable;
    private GameObject bul;
    private List<GameObject> bullets;
    private int result = 0;
    private int bulletAlternate = 2;

    [Header("BULLET DATA")]
    public BulletType bulletType;
    public float fireRate = 1;
    public int bulletsAmount = 10;
    public int bulletDamage = 1;
    public float bulletSpeed = 55;



    private void Awake()
    {
        bullets = new List<GameObject>();
    }

    public void Fire()
    {
        float angleStepA = ((endAngleA - startAngle) / bulletsAmount);
        float angleA = startAngle;

        for (int i = 0; i <= bulletsAmount; i++)
        {
            float bulDirZ = firePoint.position.x + Mathf.Sin((angleA * Mathf.PI) / 180f);
            float bulDirY = firePoint.position.y + Mathf.Cos((angleA * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirZ, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - firePoint.position).normalized;

            GameObject bul = GetBullet();
            bul.transform.position = firePoint.position;
            bul.transform.rotation = firePoint.rotation;
            EnemyBulletController bulCon = bul.GetComponent<EnemyBulletController>();
            bulCon.SetMoveDirection(bulDir);
            bulCon.speed = bulletSpeed;
            bulCon.damageToGive = bulletDamage;
            bul.transform.parent = null;

            angleA += angleStepA;
        }
    }

    public GameObject GetBullet()
    {
        if (bulletType == BulletType.NONBREAKABLE)
        {
            // Instantiate purple
            bul = Instantiate(BulletNonBreakable, firePoint);
        }
        else if (bulletType == BulletType.BREAKABLE)
        {
            // Instantiate orange
            bul = Instantiate(BulletBreakable, firePoint);
        }
        if (bulletType == BulletType.ALTERNATE && bullets.Count % bulletAlternate == result)
        {
            // Instantiate orange and purple
            bul = Instantiate(BulletNonBreakable, firePoint);
        }
        else if (bulletType == BulletType.ALTERNATE && bullets.Count % bulletAlternate != result)
        {
            // Instantiate orange and purple
            bul = Instantiate(BulletBreakable, firePoint);
        }
        bullets.Add(bul);
        return bul;
    }
}
