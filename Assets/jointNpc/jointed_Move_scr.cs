using UnityEngine;

public class jointed_move_scr : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed of left/right movement
    public float jumpForce = 7f;          // Force of the jump
    public LayerMask groundLayer;         // Layer the ground is on
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float groundCheckDistance = 0.1f;  // Distance to check below the player for ground

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Getting the Rigidbody2D component
    }

    private void Update()
    {
        Movement();  // Handle movement and jump together
    }

    void Movement()
    {
        // Handle horizontal movement with A/D or Left/Right arrow keys
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Cast a ray downwards to check if the player is grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Handle jumping with W or Space key if the player is grounded
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Visualize the ground check ray in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
