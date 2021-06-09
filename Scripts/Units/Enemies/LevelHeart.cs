using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHeart : Enemy
{
    private Renderer rend;

    private Color vulnerableColor;
    [SerializeField] private Color shieldedColor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        vulnerableColor = rend.material.GetColor("_Color");
        SetShieldedColor();
    }

    // Sets color to indicate if heart can take damage
    public void SetShieldedColor()
    {
        rend.material.SetColor("_Color", Color.gray);
        canTakeDamage = false;
    }

    public void SetVulnerableColor()
    {
        rend.material.SetColor("_Color", vulnerableColor);
        canTakeDamage = true;
    }
}
