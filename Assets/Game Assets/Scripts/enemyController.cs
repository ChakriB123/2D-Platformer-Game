using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed;
    public float rayDist;
    private bool movingRight;
    public Transform groundDefect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.KillPlayer();
            
        }
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDefect.position, Vector2.down, rayDist);
        if(groundCheck.collider == false )
        {
            if(movingRight) 
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        
    }
}
