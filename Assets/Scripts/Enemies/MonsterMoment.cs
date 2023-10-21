using System;
using System.Collections;
using UnityEngine;

public class MonsterMoment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] AudioClip punchClip;

    Rigidbody2D myRigidbody;
    EdgeCollider2D myEdgeCollider;
    CapsuleCollider2D myMainCollider;
    bool isDied = false;

    public bool IsDied => isDied;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myEdgeCollider = GetComponent<EdgeCollider2D>();
        myMainCollider = GetComponent<CapsuleCollider2D>();

        transform.localScale = new Vector2(-1f, 1f);
    }

    void Update()
    {
        if (isDied) { return; }

        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isDied) { return; }

        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other is BoxCollider2D && myEdgeCollider.IsTouching(other))
        {
            isDied = true;

            myMainCollider.enabled = false;

            myRigidbody.AddForce(new Vector2(other.gameObject.transform.localScale.x * 200f, 50f));

            transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-270, -90)));

            AudioSource.PlayClipAtPoint(punchClip, Camera.main.transform.position);

            StartCoroutine(Death());
        }
    }

    public void DestroyWithForce(Vector2 force)
    {
        isDied = true;

        myRigidbody.AddForce(force);

        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(gameObject);
    }
}
