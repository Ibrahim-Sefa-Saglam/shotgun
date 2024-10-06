using UnityEngine;

public class Shotper_scr : MonoBehaviour
{
    // Bullet prefab to shoot
    public GameObject bulletPrefab;

    // Array of bullet points and corresponding bullet origins
    public GameObject[] bulletPoints;
    public GameObject[] bulletOrigins;

    // Speed of the bullet (base speed)
    public float bulletSpeed = 20f;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < bulletPoints.Length; i++)
        {
            // Get the position of the bullet point and its corresponding bullet origin
            Vector3 firePoint = bulletPoints[i].transform.position;
            Vector3 originPoint = bulletOrigins[i].transform.position;

            // Calculate the direction vector from the bulletOrigin to the bulletPoint
            Vector2 direction = (firePoint - originPoint).normalized;

            // Instantiate the bullet at the bullet point's position
            GameObject bullet = Instantiate(bulletPrefab, firePoint, bulletPoints[i].transform.rotation);

            // Get the Rigidbody2D component from the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Set the bullet's velocity in the calculated direction
            rb.velocity = direction * bulletSpeed;
        }
    }
}
