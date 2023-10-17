using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 inputValue;
    Vector2 extraInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    BoxCollider2D feetCollider;
    InputManager inputManager;
    CinemachineVirtualCamera vcam;
    bool isAlive = true;
    float previousInputValue;

    public void UpdateExtraInput(Vector2 value)
    {
        extraInput = value;
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        inputManager = GetComponent<InputManager>();
        feetCollider = GetComponent<BoxCollider2D>();
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        Die();
        Fire();
    }

    private void Fire()
    {
        if (inputManager.IsFire)
        {
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }

    private void Die()
    {
        if (!isAlive) { return; }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Water")) || myCollider.IsTouchingLayers(LayerMask.GetMask("Plarform Enemies")))
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

            yield return new WaitForSecondsRealtime(1f);
            playerSession.RestartGame();
            SceneManager.LoadScene(0);
        }
        else
        {
            AudioSource.PlayClipAtPoint(playerDiedSfx, Camera.main.transform.position);

            yield return new WaitForSecondsRealtime(1f);
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

        var playerVelocity = new Vector2(inputValue.x * moveSpeed + extraInput.x, myRigidbody.velocity.y + extraInput.y);
        myRigidbody.velocity = playerVelocity;

        myAnimator.SetBool("isRuningLeft", hasHorizontalSpeed() && inputValue.x < 0);
        myAnimator.SetBool("isRuningRight", hasHorizontalSpeed() && inputValue.x > 0);

        if (hasHorizontalSpeed())
        {
            previousInputValue = inputValue.x;
        }
        else
        {
            myAnimator.SetInteger("IdleState", previousInputValue > 0 ? 1 : 0);
        }

        if (inputManager.IsJumping)
        {
            Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        myAnimator.SetBool("isJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            var enemiesMovement = other.gameObject.GetComponent<EnemyMoment>();
            if (enemiesMovement != null && enemiesMovement.IsDied) { return; }

            isAlive = false;
            myAnimator.SetTrigger("Dead");
            myRigidbody.velocity = deadKick;

            StartCoroutine(NewGame());
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
