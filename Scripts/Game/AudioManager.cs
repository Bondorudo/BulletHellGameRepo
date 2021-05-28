using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    //public AudioClip levelMusic;
    //[SerializeField] private float musicVolume = 0.3f;
    public AudioClip uiButtonSound;
    public AudioClip playerProjectile;
    public AudioClip playerDamage;
    public AudioClip playerDeath;
    public AudioClip bulletCollision;
    public AudioClip enemyDamage;
    public AudioClip enemyDeath;
    public AudioClip enemyProjectile;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(levelMusic, musicVolume);
        //audioSource.loop = true;
    }


    public void ButtonPressAudio()
    {
        audioSource.PlayOneShot(uiButtonSound, 0.5f);
    }

    public void BulletCollisionAudio()
    {
        audioSource.PlayOneShot(bulletCollision, 1f);
    }
   
    public void PlayerProjectileAudio()
    {
        audioSource.PlayOneShot(playerProjectile, 0.1f);
    }

    public void PlayerDeathAudio()
    {
        audioSource.PlayOneShot(playerDeath, 0.5f);
    }

    public void PlayerDamageAudio()
    {
        audioSource.PlayOneShot(playerDamage, 0.4f);
    }

    public void EnemyProjectileAudio()
    {
        audioSource.PlayOneShot(enemyProjectile, 0.2f);
    }

    public void EnemyDeathAudio()
    {
        audioSource.PlayOneShot(enemyDeath, 0.6f);
    }

    public void EnemyDamageAudio()
    {
        audioSource.PlayOneShot(enemyDamage, 0.3f);
    }
}
