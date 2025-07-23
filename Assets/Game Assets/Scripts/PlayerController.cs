using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D playerCollider;

    private float originalHeight;
    private float crouchHeight = 1.2f;

    public void Start()
    {
        originalHeight = playerCollider.size.y;
    }
    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));
        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        float verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0)
        {
            animator.SetBool("Jump" , true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        bool isCrouch = Input.GetButton("Crouch");
        
        if(isCrouch)
        {
            animator.SetBool("Crouch", true);
            playerCollider.size = new Vector2(playerCollider.size.x, crouchHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, crouchHeight / 2);
        }
        else
        {
            Debug.Log("nocrouuch");
            animator.SetBool("Crouch", false);
            playerCollider.size = new Vector2(playerCollider.size.x, originalHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, originalHeight / 2);
        }

    }
}
