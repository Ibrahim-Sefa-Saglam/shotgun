using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the character movement
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    private Animator animator;  // Reference to the Animator component
    private bool isMovingRight = false;  // Tracks whether the player is moving right

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D attached to the GameObject
        animator = GetComponent<Animator>();  // Get the Animator attached to the GameObject
    }

    void Update()
    {
        // Get horizontal input (left and right arrow keys, or "A" and "D")
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        // Move the player by setting the Rigidbody's velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Check if the player is moving
        if (moveInput != 0)
        {
            // If moving right
            if (moveInput > 0)
            {
                if (!isMovingRight)
                {
                    // Set the player's direction to face right
                    transform.localScale = new Vector3(1, 1, 1);
                    isMovingRight = true;
                }
                animator.SetBool("isWalking", true);  // Trigger walking animation
            }
            // If moving left
            else if (moveInput < 0)
            {
                if (isMovingRight)
                {
                    // Set the player's direction to face left (flip sprite)
                    transform.localScale = new Vector3(-1, 1, 1);
                    isMovingRight = false;
                }
                animator.SetBool("isWalking", true);  // Still trigger walking animation even when moving left
            }
        }
        else
        {
            // If no movement input, stop the walking animation
            animator.SetBool("isWalking", false);
        }
    }
}
