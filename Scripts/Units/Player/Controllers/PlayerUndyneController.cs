using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUndyneController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        if (xInput == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (xInput == -1)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (zInput == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (zInput == -1)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
}
