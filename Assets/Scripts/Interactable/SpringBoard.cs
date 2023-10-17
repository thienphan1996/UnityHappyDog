using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] AudioClip jumpSfx;

    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other is CapsuleCollider2D)
        {
            other.attachedRigidbody.velocity += new Vector2(0f, jumpSpeed);

            myAnimator.SetBool("isUp", !myAnimator.GetBool("isUp"));

            AudioSource.PlayClipAtPoint(jumpSfx, Camera.main.transform.position);
        }
    }

    public void OnEndUpAnimation()
    {
        myAnimator.SetBool("isUp", false);
    }
}
