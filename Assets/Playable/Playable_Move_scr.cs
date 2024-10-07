using UnityEngine;

public class Playable_move_scr : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed of left/right movement
    public float jumpForce = 7f;          // Force of the jump
    public LayerMask groundLayer;         // Layer the ground is on
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float groundCheckDistance = 0.2f;  // Distance to check below the player for ground (increased)
    private float coyoteTime = 0.1f;      // Allow the player to jump shortly after leaving the ground
    private float coyoteTimeCounter;      // Tracks how long the player has been off the ground

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check if the raycast hit something
        if (hit.collider != null)
        {
            isGrounded = true;
            coyoteTimeCounter = coyoteTime;  // Reset coyote time when grounded
            Debug.Log("Grounded on: " + hit.collider.gameObject.name);  // Log the object the raycast hits
        }
        else
        {
            isGrounded = false;
        }

        // Start coyote time countdown when leaving the ground
        if (!isGrounded)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Handle jumping with W or Space key if the player is grounded or within coyote time
        if (coyoteTimeCounter > 0 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            // Set vertical velocity to 0 to ensure consistent jumping
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0;  // Reset coyote time after jumping
        }
    }

    // Visualize the ground check ray in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
