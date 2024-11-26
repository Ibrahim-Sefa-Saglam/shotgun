using UnityEngine;

// Main script for explosion behavior
public class Trying : MonoBehaviour
{
    public float explosionRadius = 5f;     // Radius of the explosion
    public float explosionForce = 10f;     // Maximum force applied to nearby objects
    public int rayCount = 360;              // Number of rays to send in the 360-degree scan
    public LayerMask layerMask;             // Layer mask to define which objects should be affected

    private Stack_for_exp objectStack;      // Stack to store objects hit by the explosion
    public float explosionDuration = 1f; // Duration the explosion force is applied
    private float explosionTimer;            // Timer to track how long the explosion has been active

    void Start()
    {
        objectStack = new Stack_for_exp();
        
        
        explosionTimer = 0f; // Initialize the timer
    }

    // Method to handle the explosion scanning
    void Explode()
    {
        float angleStep = 360f / rayCount; // Divide 360 degrees by the number of rays
        Vector2 originPosition = transform.position;

        for (int i = 0; i < rayCount; i++)
        {   
            float currentAngle = i * angleStep;
            Vector2 direction = GetDirectionFromAngle(currentAngle);
            RaycastHit2D hit = Physics2D.Raycast(originPosition, direction, explosionRadius, layerMask);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                objectStack.Push(Vector2.Distance(originPosition, hit.point), hitObject);

            }
            else
            {
            }
        }
    }

    // Convert angle to a direction vector
    Vector2 GetDirectionFromAngle(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    // Apply force to objects in the stack
    void ApplyExplosionForce()
    {
        Node currentNode = objectStack.head; // Start from the head of the stack

        while (currentNode != null) // Iterate through all nodes
        {
            GameObject obj = currentNode.gameObject; // Access the GameObject property

            if (obj != null)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Calculate distance from the explosion origin
                    float distance = Vector2.Distance(transform.position, obj.transform.position);
                    // Calculate the force to apply
                    float forceToApply = Mathf.Max(0, (1 - distance / explosionRadius) * explosionForce);
                    // Calculate the direction to apply the force
                    Vector2 direction = (obj.transform.position - transform.position).normalized;

                    // Apply the force
                    rb.AddForce(direction * forceToApply, ForceMode2D.Impulse);
                }
            }

            currentNode = currentNode.next; // Move to the next node in the stack
        }
    }

    void Update()
    {
        // Update the explosion timer
        explosionTimer += Time.deltaTime;

        // Check if the explosion duration has passed
        if (explosionTimer >= explosionDuration)
        {
       
        Explode();
        ApplyExplosionForce();

         Destroy(gameObject);   
        }
    }
}
