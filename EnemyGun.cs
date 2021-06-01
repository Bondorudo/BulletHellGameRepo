using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private float[][] firePointPos = new float[4][];
    private int[] firePointRot = new int[4] { 0, 180, 90, 270};

    private float[][] firePointTrianglePos = new float[3][];
    private int[] firePointTriangleRot = new int[3] { 180, -58, 58 };

    [Header("FIREPOINTS")]
    [Range(1, 4)]
    [SerializeField] private int firePointCount;

    [Header("BULLET DATA")]
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float alternateSpeed;
    private int bulletDamage = 1;

    [Header("BULLETS")]
    [SerializeField] private GameObject BulletBreakable;
    [SerializeField] private GameObject BulletNonBreakable;
    GameObject bullet;

    private Vector3 shootDir;

    private void Start()
    {
        // Initialize firePoint positions with arrays depending on how many firepoints should exist
        firePointPos[0] = new float[] { 0f, 0f, 1.5f };
        firePointPos[1] = new float[] { 0f, 0f, -1.5f };
        firePointPos[2] = new float[] { 1.5f, 0f, 0f };
        firePointPos[3] = new float[] { -1.5f, 0f, 0f };

        firePointTrianglePos[0] = new float[] { 0f, 0f, -1.5f};
        firePointTrianglePos[1] = new float[] { -1.6f, 0f, 1f };
        firePointTrianglePos[2] = new float[] { 1.6f, 0f, 1f };

        BulletToShoot();

        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while (this.isActiveAndEnabled)
        {
            CreateFirePoints();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void CreateFirePoints()
    {
        if (firePointCount != 3)
        {
            for (int i = 0; i < firePointCount; i++)
            {
                Vector3 position = new Vector3(firePointPos[i][0], firePointPos[i][1], firePointPos[i][2]);
                GameObject newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, firePointRot[i], 0));
                newBullet.transform.SetParent(gameObject.transform);
                newBullet.transform.localPosition = position;
                newBullet.transform.parent = null;
                shootDir = (newBullet.transform.position - this.gameObject.transform.position).normalized;
                EnemyBulletController bulletStats = newBullet.gameObject.GetComponent<EnemyBulletController>();
                bulletStats.SetUp(shootDir);
                bulletStats.speed = bulletSpeed;
                bulletStats.damageToGive = bulletDamage;
            }
        }
        else
        {
            for (int i = 0; i < firePointCount; i++)
            {
                Vector3 position = new Vector3(firePointTrianglePos[i][0], firePointTrianglePos[i][1], firePointTrianglePos[i][2]);

                GameObject firePoint = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, firePointTriangleRot[i], 0));
                firePoint.transform.SetParent(gameObject.transform);
                firePoint.transform.localPosition = position;
            }
        }
    }

    private GameObject BulletToShoot()
    {
        if (bulletType == BulletType.BREAKABLE)
        {
            bullet = BulletBreakable;
            return bullet;
        }
        else if (bulletType == BulletType.NONBREAKABLE)
        {
            bullet = BulletNonBreakable;
            return bullet;
        }
        else if (bulletType == BulletType.ALTERNATE)
        {
            StartCoroutine(Alternate());
            return bullet;
        }
        return null;
    }

    IEnumerator Alternate()
    {
        bool alternate = true;
        while (this.isActiveAndEnabled)
        {
            if (alternate) bullet = BulletBreakable;
            else bullet = BulletNonBreakable;

            yield return new WaitForSeconds(fireRate);
            alternate = !alternate;
        }
    }
}
