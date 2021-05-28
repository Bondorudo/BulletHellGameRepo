using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Enemy
{
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
