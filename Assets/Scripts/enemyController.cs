using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField] private float rayDist;
    [SerializeField] bool movingRight;
    [SerializeField] private Transform groundDefect;
    [SerializeField] private Animator enemyAnimator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            SoundManager.Instance.play(SoundsEnum.EnemyAttack);
            playerController.DecreaseHealth();
        }
    }
    private void Update()
    {
        patrolEnemy();
    }

    private void patrolEnemy()
    {
        enemyAnimator.SetBool("IsPatrol", true);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDefect.position, Vector2.down, rayDist);
        if (groundCheck.collider == false)
        {
            if (movingRight)
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
