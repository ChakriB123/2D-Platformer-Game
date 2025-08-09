using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider: MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 5f;
    private Vector2 targetPosition;

    void Update()
    {
        
            // Keep current Y, match player’s X
            targetPosition = new Vector2(playerTransform.position.x, transform.position.y);

            // Smoothly move the platform to the target position
            transform.position = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
      

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
            
    }
}
