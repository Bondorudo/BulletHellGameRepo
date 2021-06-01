using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private bool isTouchingWall;

    // When colliding with wall set isTouchingWall to true
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("MovingWall"))
        {
            isTouchingWall = true;
        }
    }

    // When not colliding with wall set isTouchingWall to false
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("MovingWall"))
        {
            isTouchingWall = false;
        }
    }

    // Get isTouchingWall
    public bool IsTouchingWall()
    {
        if (isTouchingWall)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}