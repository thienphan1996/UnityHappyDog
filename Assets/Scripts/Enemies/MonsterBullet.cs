using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float lifeDistance = 2f;

    Rigidbody2D myRigidbody;
    float xScaleType = 1;
    float firstX = 0f;

    public void SetXScaleType(float scaleType)
    {
        xScaleType = scaleType;
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        firstX = transform.position.x;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed * xScaleType, 0f);

        if (transform.position.x > firstX + lifeDistance || transform.position.x < firstX - lifeDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Dead();
        }

        Destroy(gameObject);
    }
}
