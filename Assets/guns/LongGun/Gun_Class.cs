using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GameObject[] holdPoints; //1
    public GameObject bulletPoint;//2
    public GameObject bulletPrefab;//3
    // Gun properties
    public float bulletSpeed;// Speed of the bullet 4
    public float spreadAngle;// Angle spread for shooting 5
    public int bulletCount;// Number of bullets fired per shot 6
    public int holdDistance; // 7

    // Abstract methods that need to be implemented by child classes
    public abstract void Shoot();  // Function to handle shooting bullets
    public abstract void Reload(); // Function to handle reloading the gund
    public virtual void InfoUpdate(){ // Update the current guns attribute< information in Gun_Info file
    Gun_Info gunInfo = GetComponent<Gun_Info>();
    if (gunInfo == null) return;
    if (holdPoints != null && holdPoints.Length > 0) gunInfo.holdPoints = holdPoints; // 1
    if (bulletPoint != null) gunInfo.bulletPoint = bulletPoint;// 2
    if (bulletPrefab != null) gunInfo.bulletPrefab = bulletPrefab;// 3
    if (bulletSpeed > 0) gunInfo.bulletSpeed = bulletSpeed;// 4
    if (spreadAngle >= 0) gunInfo.spreadAngle = spreadAngle;// 5
    if (bulletCount > 0) gunInfo.bulletCount = bulletCount;//6
    if (holdDistance > 0) gunInfo.holdDistance = holdDistance;//7
    }

    
}
