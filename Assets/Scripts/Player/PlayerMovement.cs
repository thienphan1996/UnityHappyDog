using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] Vector2 deadKick = new Vector2(20f, 20f);
    [SerializeField] AudioClip jumpSfx;
    [SerializeField] AudioClip playerDiedSfx;
    [SerializeField] AudioClip gameOverSfx;

    Vector2 inputValue;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    BoxCollider2D feetCollider;
    InputManager inputManager;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        inputManager = GetComponent<InputManager>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        Die();
    }

    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dead");
            myRigidbody.velocity = deadKick;

            StartCoroutine(NewGame());
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Fall");
            myRigidbody.velocity = deadKick;

            StartCoroutine(NewGame());
        }
    }
    IEnumerator NewGame()
    {
        var playerSession = FindObjectOfType<PlayerSession>();
        if (playerSession.PlayerDeath() == 0)
        {
            AudioSource.PlayClipAtPoint(gameOverSfx, Camera.main.transform.position);

            yield return new WaitForSecondsRealtime(2f);
            playerSession.RestartGame();
            SceneManager.LoadScene(0);
        }
        else
        {
            AudioSource.PlayClipAtPoint(playerDiedSfx, Camera.main.transform.position);

            yield return new WaitForSecondsRealtime(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FlipSprite()
    {
        if (hasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(inputValue.x), 1f);
        }
    }

    private bool hasHorizontalSpeed()
    {
        return Mathf.Abs(inputValue.x) > Mathf.Epsilon;
    }

    private void Run()
    {
        inputValue = inputManager.MoveInput.normalized;

        var playerVelocity = new Vector2(inputValue.x * moveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        myAnimator.SetBool("isRuning", hasHorizontalSpeed());

        if (inputManager.IsJumping)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            myAnimator.SetBool("isJumping", false);
        }
    }

    void Jump()
    {
        if (!isAlive) { return; }

        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            myAnimator.SetBool("isJumping", true);

            AudioSource.PlayClipAtPoint(jumpSfx, Camera.main.transform.position);
        }
    }
}
