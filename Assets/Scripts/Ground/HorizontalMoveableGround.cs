using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveableGround : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float length = 10f;

    Rigidbody2D myRb;
    float firstX;
    float speed = 0f;
    PlayerMovement playerMovement;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        firstX = transform.position.x;
        speed = moveSpeed;
    }

    void Update()
    {
        if (transform.position.x > firstX + length)
        {
            speed = -moveSpeed;
        }
        else if (transform.position.x < firstX - length)
        {
            speed = moveSpeed;
        }

        myRb.velocity = new Vector2(speed, 0f);

        if (playerMovement != null)
        {
            playerMovement.UpdateExtraInput(new Vector2(speed, 0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (playerMovement != null)
        {
            playerMovement.UpdateExtraInput(new Vector2(0f, 0f));
            playerMovement = null;
        }
    }
}
