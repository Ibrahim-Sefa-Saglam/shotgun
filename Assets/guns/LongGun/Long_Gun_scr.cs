using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Gun_scr : Gun
{
    

    void Start()
    {
    if(bulletSpeed == 0)bulletSpeed = 15;
    if(bulletCount == 0)bulletCount = 10;
    if(spreadAngle == 0f)spreadAngle = 10;       
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
 public override void Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // Get the position of the bulletPoint
            Vector3 firePoint = bulletPoint.transform.position;

            // Calculate the direction from the gun's center to the bulletPoint
            Vector2 bulletDirection = (bulletPoint.transform.position - transform.position).normalized;

            // Generate a random angle between -spreadAngle and +spreadAngle
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);

            // Rotate the bulletDirection by the random angle
            Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
            Vector2 rotatedDirection = rotation * bulletDirection;

            // Instantiate the bullet at the bulletPoint's position with the same rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint, bulletPoint.transform.rotation);

            // Get the Rigidbody2D component from the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Generate a random speed for the bullet between maxSpeed * (9/10) and maxSpeed * (11/10)
            float randomSpeed = Random.Range(bulletSpeed * 0.9f, bulletSpeed * 1.1f);

            // Set the bullet's velocity in the rotated direction with the random speed
            rb.velocity = rotatedDirection * randomSpeed;
        }
    }
 public override void Reload(){return;}

}

