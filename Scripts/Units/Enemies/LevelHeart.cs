using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHeart : Enemy
{
    private Renderer rend;

    private Color storedColor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
        canTakeDamage = false;
        SetShieldedColor();
    }

    // Set can take damage to true
    public void CanTakeDamage()
    {
        canTakeDamage = true;
    }

    // Sets color to indicate if heart can take damage
    public void SetShieldedColor()
    {
        rend.material.SetColor("_Color", Color.gray);
    }

    public void SetVulnerableColor()
    {
        rend.material.SetColor("_Color", storedColor);
    }
}
