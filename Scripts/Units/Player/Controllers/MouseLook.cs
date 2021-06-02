using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameManager gm;
    public Transform player;

    public float mouseSensitivity { get; private set; }

    private float y = 0;

    public static MouseLook instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = 0;
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

    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity *= value * 100;

        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
        PlayerPrefs.Save();
    }
}
