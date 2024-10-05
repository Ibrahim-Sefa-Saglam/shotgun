using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1_script : MonoBehaviour
{
    // Time in seconds before the bullet is destroyed
    public float lifetime = 10f;

    // Reference to the Rigidbody2D component
    public Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component attached to the bullet

        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // If the bullet has a velocity, rotate it to face the direction of the velocity
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
