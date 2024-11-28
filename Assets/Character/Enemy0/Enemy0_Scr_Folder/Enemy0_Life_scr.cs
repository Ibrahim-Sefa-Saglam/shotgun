using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_Life : MonoBehaviour, IDamageable
{
    public int Health { get; set; }
    public Enemy0_Behavior_scr enemy0_Behavior;
    public int testingDamage = 10;


    void Start()
    {
        Health = 100;
    }

    void Update()
    {   
        if (Health <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) {
            TakeDamage(testingDamage);  
            Destroy(collision.gameObject);
        } 
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        enemy0_Behavior.TakeDmg(true);
    }
    public void Die()
    {
        enemy0_Behavior.Die(true);
    }
    public void TakeDamage(){}
}