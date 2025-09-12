using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    [SerializeField] private Image[] hearts;

    [SerializeField] private GameObject deathUIPanel;
    private Camera mainCamera;

    public ScoreController scoreController;
    public GameOverController gameOverController;
    private bool isDead = false;

    public float speed;
    public float jumpForce;
    private int health;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;
    private bool doubleJump;

    private float originalHeight;
    private float crouchHeight = 1.2f;

    private void Awake()
    {
        playerCollider =gameObject.GetComponent<BoxCollider2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        health = hearts.Length;

        mainCamera = Camera.main;
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
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
    }
    private void MoveCharacter(float horizontal, float vertical)
    {
        //Move charactor Horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        // for double jump
        if(isGrounded && !Input.GetButton("Jump") ){

            doubleJump = false;
        }
        //Move charactor Vertically
        if (Input.GetButtonDown("Jump") )
        {
            if (isGrounded || doubleJump)
            {
                SoundManager.Instance.play(SoundsEnum.PlayerJump);
                playerAnimator.SetTrigger("Jump");
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
        }
    }
    private void PlayMovementAnimations(float horizontal, float vertical)
    {
        
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
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
            playerAnimator.SetBool("Crouch", true);
            playerCollider.size = new Vector2(playerCollider.size.x, crouchHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, crouchHeight / 2);
        }
        else
        {
            playerAnimator.SetBool("Crouch", false);
            playerCollider.size = new Vector2(playerCollider.size.x, originalHeight);
            playerCollider.offset = new Vector2(playerCollider.offset.x, originalHeight / 2);
        }
    }

    public void PickupKey()
    {
        Debug.Log("Picked up key ");
        scoreController.incrementScore(2);
    }
 
    public void DecreaseHealth()
    {
        health--;

        HandleHealthUI();
        if (health <= 0)
        {
            PlayDeathAnimation();
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        isDead = true;
        mainCamera.transform.parent = null;
       // deathUIPanel.gameObject.SetActive(true);
        gameOverController.PlayerDied();
       // playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void PlayDeathAnimation()
    {
        playerAnimator.SetTrigger("Die");
    }

    public void HandleHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = (i < health) ? Color.red : Color.black;
        }
    }

}
