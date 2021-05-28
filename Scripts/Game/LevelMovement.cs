using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Vector3 startPos;
    private float repeathWidth;

    private void Start()
    {
        startPos = transform.position;
        repeathWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < startPos.x - repeathWidth)
        {
            transform.position = startPos;
        }
    }
}
