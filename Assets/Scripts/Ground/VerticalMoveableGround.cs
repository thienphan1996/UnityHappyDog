using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveableGround : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float length = 10f;
    [SerializeField] bool autoMove = true;

    Rigidbody2D myRb;
    float firstY;
    float speed = 0f;
    public void Go()
    {
        speed = moveSpeed;
    }

    public void Return()
    {
        speed = -moveSpeed;
    }

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        firstY = transform.position.y;

        if (autoMove)
        {
            speed = moveSpeed;
        }
    }

    void Update()
    {
        if (!autoMove)
        {
            if (moveSpeed > 0)
            {
                if (speed > 0 && transform.position.y > firstY + length)
                {
                    speed = 0;
                }
                if (speed < 0 && transform.position.y < firstY)
                {
                    speed = 0;
                }
            }
            if (moveSpeed < 0)
            {
                if (speed < 0 && transform.position.y < firstY - length)
                {
                    speed = 0;
                }
                if (speed > 0 && transform.position.y > firstY)
                {
                    speed = 0;
                }
            }
        }
        else
        {
            if (transform.position.y > firstY + length)
            {
                speed = -moveSpeed;
            }
            else if (transform.position.y < firstY - length)
            {
                speed = moveSpeed;
            }
        }

        myRb.velocity = new Vector2(0f, speed);
    }
}
