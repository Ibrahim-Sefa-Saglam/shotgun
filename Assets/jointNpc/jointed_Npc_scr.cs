using UnityEngine;

public class jointed_npc_scr : MonoBehaviour
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
        Move();
        Jump();
    }

    void Move()
    {
        float moveDirection = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow keys
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);  // Move player horizontally
    }

    void Jump()
    {
        // Cast a ray downwards to check if the player is grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Jump if W is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
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
