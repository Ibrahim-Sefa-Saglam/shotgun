using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude_Move: MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool moveLeft = Input.GetKey("a"); 
        bool moveRight = Input.GetKey("d");
        Movement(moveLeft, moveRight);
    }
    void Movement(bool isLeftKey, bool isRightKey)
    {   

        if (isLeftKey) rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        else if (isRightKey)rb.velocity = new Vector2(moveSpeed, rb.velocity.y);      
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
    }
}
