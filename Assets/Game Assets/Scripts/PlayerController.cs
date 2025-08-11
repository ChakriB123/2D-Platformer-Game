using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D playerCollider;

    public ScoreController scoreController;
    public float speed;
    public float jumpForce;
    public int lifes;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;


    private Rigidbody2D rb2D;
    private bool isGrounded;

    private float originalHeight;
    private float crouchHeight = 1.2f;

    private void Awake()
    {
        playerCollider =gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        originalHeight = playerCollider.size.y;
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        PlayMovementAnimations(horizontal, vertical);

        bool isCrouch = Input.GetKey(KeyCode.LeftControl);
        PlayCrouchAnimation(isCrouch);

        MoveCharacter(horizontal, vertical);

    }
    private void MoveCharacter(float horizontal, float vertical)
    {
        //Move charactor Horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;


        //Move charactor Vertically
        if (vertical > 0 && isGrounded)
        {
            animator.SetTrigger("Jump");
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            //rb2D.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
        }
    }
    private void PlayMovementAnimations(float horizontal, float vertical)
    {
        
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

    }

    private void PlayCrouchAnimation(bool Crouch)
    {
        if (Crouch)
        {
            animator.SetBool("Crouch", true);
            playerCollider.size = new Vector2(playerCollider.size.x, crouchHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, crouchHeight / 2);
        }
        else
        {
            animator.SetBool("Crouch", false);
            playerCollider.size = new Vector2(playerCollider.size.x, originalHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, originalHeight / 2);
        }
    }

    public void PickupKey()
    {
        Debug.Log("Picked up key ");
        scoreController.incrementScore(2);
    }

    public void KillPlayer()
    {
        Debug.Log("Enemy attacked");
        Destroy(gameObject);
        reloadLevel();
    }

    public void reloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void DamagePlayer()
    {
        if (lifes < 1)
        {
            KillPlayer();
        }
        else
        {
            lifes--;
            Debug.Log("lifes after damage:" + lifes);
        }
    }
}
