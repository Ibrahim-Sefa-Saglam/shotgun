using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Info:MonoBehaviour // THIS HOLDS THE VALUES OF THE ATTRIBUTES FOR DISPLAY
{    
    public GameObject[] holdPoints;     
    public GameObject bulletPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed; // Speed of the bullet
    public float spreadAngle; // Angle spread for shooting
    public int bulletCount; // Number of bullets fired per shot 
    public int holdDistance;
}
