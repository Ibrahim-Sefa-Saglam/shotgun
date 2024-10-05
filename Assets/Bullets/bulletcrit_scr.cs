using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletcCrit_Script : MonoBehaviour
{
    // Time in seconds before the bullet is destroyed

    // Reference to the Rigidbody2D component
    public Rigidbody2D rb;
    public GameObject explotionPrefab;
     public float lifetime = 10f;

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
    private void OnCollisionEnter2D(Collision2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) != "eq_gun")
        {GameObject explode = Instantiate(explotionPrefab, transform.position,Quaternion.identity); 
        Destroy(gameObject);
        }
        
        
    }
}
