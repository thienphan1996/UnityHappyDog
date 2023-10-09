using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlantMoment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        transform.localScale = new Vector2(-1f, 1f);
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}
