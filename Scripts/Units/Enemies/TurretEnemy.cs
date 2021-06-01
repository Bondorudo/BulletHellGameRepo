using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    [Header("Rotation")]
    [SerializeField] private bool rotateClockwise;
    [SerializeField] private float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        if (rotateClockwise == true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (rotateClockwise == false)
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
    }
}
