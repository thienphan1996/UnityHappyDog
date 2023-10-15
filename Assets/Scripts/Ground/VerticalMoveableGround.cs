using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveableGround : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float length = 10f;

    Rigidbody2D myRb;
    float firstY;
    float speed = 0f;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        firstY = transform.position.y;
        speed = moveSpeed;
    }

    void Update()
    {
        if (transform.position.y > firstY + length)
        {
            speed = -moveSpeed;
        }
        else if (transform.position.y < firstY - length)
        {
            speed = moveSpeed;
        }

        myRb.velocity = new Vector2(0f, speed);
    }
}
