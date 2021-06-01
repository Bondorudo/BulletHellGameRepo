using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    public GameObject enemyBody;

    // Update is called once per frame
    void Update()
    {
        if (enemyBody == null)
        {
            Destroy(gameObject);
        }
    }
}
