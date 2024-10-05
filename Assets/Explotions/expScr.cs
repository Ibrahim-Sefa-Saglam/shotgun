using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expScr : MonoBehaviour
{
    Collider2D[] inExplosionRadius = null; // Fixed variable name to match the function
    [SerializeField] private float ExplosionForceMulti = 5f; // Multiplier for explosion force
    [SerializeField] private float ExplosionRad = 5f; // Radius of the explosion

    // Start is called before the first frame update
    void Start()
    {
        // Call the explode method for testing (You can adjust when to call this based on your needs)
          
        {
            Explode(); // Call the explode method
            Destroy(gameObject); // Destroy this object after the explosion
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can call the Explode method here if you want to trigger it every frame, but it might be more efficient to trigger it under specific conditions.
    }

    void Explode()
    {
        // Get all colliders within the explosion radius
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRad);
        
        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_body = o.GetComponent<Rigidbody2D>(); // Corrected this line
            if (o_body != null) // Check if the Rigidbody2D exists
            {
                Vector2 distanceVector = o.transform.position - transform.position; // Fixed typo here
                if (distanceVector.magnitude > 0)
                {
                    // Calculate the explosion force based on the distance
                    float explosionForce = ExplosionForceMulti / distanceVector.magnitude;
                    o_body.AddForce(distanceVector.normalized * explosionForce); // Corrected to use o_body
                }
            }
        }
    }
}
