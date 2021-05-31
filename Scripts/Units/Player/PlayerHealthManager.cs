using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private Renderer rend;
    private GameManager gameManager;
    private AudioManager audioManager;
    [SerializeField] private ParticleSystem explosionParticle;

    public float flashLength;
    private float flashCounter;

    private int wallDamage = 1;
    private int startingHealth = 3;
    public int currentHealth;

    private Color storedColor;


    void Start()
    {
        // Get a reference to components, set currentHealth to startingHealth, set player hp indicators active.
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindWithTag("SFX").GetComponent<AudioManager>();
        rend = GetComponentInChildren<Renderer>();
        storedColor = rend.material.GetColor("_Color");
        currentHealth = startingHealth;
        transform.Find("Health_1").gameObject.SetActive(true);
        transform.Find("Health_2").gameObject.SetActive(true);
    }
    
    void Update()
    {
        // Game over happens if player health is equal to 0
        if (currentHealth <= 0)
        {
            audioManager.PlayerDeathAudio();
            rend.material.SetColor("_Color", storedColor);
            explosionParticle.transform.parent = null;
            if (!explosionParticle.isPlaying) explosionParticle.Play();
            gameManager.GameOver();
        }

        // Deactivate hp indicators based on player hp
        if (currentHealth == 2)
        {
            transform.Find("Health_2").gameObject.SetActive(false);
        }
        if (currentHealth == 1)
        {
            transform.Find("Health_1").gameObject.SetActive(false);
        }

        // Player color is restored to normal
        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", storedColor);
            }
        }
    }

    // When player is hurt they lose health and their color turns red
    public void HurtPlayer(int damageAmount)
    {
        // If iFrame counter is less than zero player can take damage again.
        if (flashCounter <= 0)
        {
            audioManager.PlayerDamageAudio();
            currentHealth -= damageAmount;
            rend.material.SetColor("_Color", Color.red);
            flashCounter = flashLength;
        }
    }

    // Decrease Health when colliding with damaging walls and enemies
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DamageWall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shield")
        {
            HurtPlayer(wallDamage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "DamageWall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shield")
        {
            HurtPlayer(wallDamage);
        }
    }
}
