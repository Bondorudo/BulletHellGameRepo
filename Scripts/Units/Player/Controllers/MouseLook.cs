using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameManager gm;
    public Transform player;

    public float mouseSensitivity;

    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.pauseGame)
        {
            y += Input.GetAxis("Mouse X") * mouseSensitivity;

            player.transform.localRotation = Quaternion.Euler(0, y, 0);
        }
    }
}
