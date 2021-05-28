using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentPlayerData : MonoBehaviour
{
    public int level;
    public int health;

    private PlayerHealthManager playerHealth;


    private void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealthManager>();
    }

    void Update()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        health = playerHealth.currentHealth;
    }
}
