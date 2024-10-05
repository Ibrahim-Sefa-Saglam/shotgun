using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Bullet prefab to shoot
    public GameObject bulletPrefab;
    public GameObject bullet_point;

    // Speed of the bullet (maxSpeed is the base speed)
    public float bulletSpeed = 20f;

    // Number of bullets to spawn
    public int bulletCount = 7;

    // Maximum spread angle in degrees
    public float maxSpreadAngle = 15f;

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
        for (int i = 0; i < bulletCount; i++)
        {
            // Get the position of the bullet_point
            Vector3 firePoint = bullet_point.transform.position;

            // Calculate the direction from the gun's center to the bullet_point
            Vector2 bulletDirection = (bullet_point.transform.position - transform.position).normalized;

            // Generate a random angle between -maxSpreadAngle and +maxSpreadAngle
            float randomAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);

            // Rotate the bulletDirection by the random angle
            Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
            Vector2 rotatedDirection = rotation * bulletDirection;

            // Instantiate the bullet at the bullet_point's position with the same rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint, bullet_point.transform.rotation);

            // Get the Rigidbody2D component from the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Generate a random speed for the bullet between maxSpeed * (9/10) and maxSpeed * (11/10)
            float randomSpeed = Random.Range(bulletSpeed * 0.9f, bulletSpeed * 1.1f);

            // Set the bullet's velocity in the rotated direction with the random speed
            rb.velocity = rotatedDirection * randomSpeed;
        }
    }
}
