using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Enemy
{
    private void Start()
    { 
        // Get reference to rigidbody
        rb = GetComponent<Rigidbody>();
    }
}
