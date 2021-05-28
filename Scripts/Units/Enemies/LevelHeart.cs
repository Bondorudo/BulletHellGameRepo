using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHeart : MonoBehaviour
{
    private EnemyManager enemyManager;

    private Renderer rend;

    private Color storedColor;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
        enemyManager.canTakeDamage = false;
        SetShieldedColor();
    }

    public void CanTakeDamage()
    {
        enemyManager.canTakeDamage = true;
    }

    public void SetShieldedColor()
    {
        rend.material.SetColor("_Color", Color.gray);
    }

    public void SetVulnerableColor()
    {
        rend.material.SetColor("_Color", storedColor);
    }
}
