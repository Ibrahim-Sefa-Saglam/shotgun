using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GameObject[] holdPoints; 
    public GameObject bulletPoint;
    public GameObject bulletPrefab;
    // Gun properties
    public float bulletSpeed;       // Speed of the bullet
    public float spreadAngle;       // Angle spread for shooting
    public int bulletCount;         // Number of bullets fired per shot
    public int holdDistance;

    // Abstract methods that need to be implemented by child classes
    public abstract void Shoot();  // Function to handle shooting bullets
    public abstract void Reload(); // Function to handle reloading the gun

    // Optionally, you can add some common functionality for all guns here

    
}
