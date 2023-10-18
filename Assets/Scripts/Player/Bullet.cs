using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    Rigidbody2D myRigidbody;
    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        var playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            xSpeed = playerMovement.transform.localScale.x * moveSpeed;
            transform.Rotate(new Vector3(0f, 0f, playerMovement.transform.localScale.x * -90));
        }
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }

        if (other.CompareTag("Enemies"))
        {
            other.GetComponent<EnemyMoment>().DestroyWithForce(new Vector2(xSpeed * 30f, 100f));
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyEnemy(GameObject enemy)
    {
        yield return new WaitForSecondsRealtime(1);

        Destroy(enemy);
    }
}
