using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_Move_scr : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private string currentState;
    private string Idle_Anim = "Enemy0_Idle_anim";
    private string Move_anim = "Enemy0_Move_anim";
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = Idle_Anim;
    }
    void Update()
    {
        bool moveLeft = Input.GetKey("a"); 
        bool moveRight = Input.GetKey("d");
        Movement(moveLeft, moveRight);
    }
    void Movement(bool isLeftKey, bool isRightKey)
    {   
        Vector3 scale = transform.localScale;

        if (isLeftKey){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            scale.x = Mathf.Abs(scale.x); // Ensure facing left
            transform.localScale = scale;                
        }
        else if (isRightKey){
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            scale.x = -Mathf.Abs(scale.x); // Ensure facing right
            transform.localScale = scale;
        }        
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimState(Idle_Anim);
            return;
        }
        ChangeAnimState(Move_anim);
    }
    void ChangeAnimState(string newState){
        // guards the current animation from itself
        if(currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
