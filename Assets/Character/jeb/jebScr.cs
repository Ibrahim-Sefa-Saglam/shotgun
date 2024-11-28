using UnityEngine;

public class jebScr : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the character movement
    
    public GameObject gunPoint;   // The position where the gun will be attached
    public GameObject gunAtHand;  // Reference to the gun the player is holding
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component
    private bool isMovingRight = false;  // Tracks whether the player is moving right

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D attached to the GameObject
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
            }
        }

        // If gun is equipped, set its position to always follow the gunPoint
        if (gunAtHand != null)
        {
            gunAtHand.transform.position = gunPoint.transform.position;
        }
    }

    // Detect collisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "gun"
        if (collision.gameObject.CompareTag("gunLayer"))
        {
            // Check if the player is not already holding a gun
            if (gunAtHand == null)
            {
                // Set the gunAtHand to the collided gun object
                gunAtHand = collision.gameObject;

                // Set the gun's position to the gunPoint's position
                gunAtHand.transform.position = gunPoint.transform.position;

                // Change the gun's layer to "equipt"
                gunAtHand.layer = LayerMask.NameToLayer("equipt");

            }
        }
    }
}
