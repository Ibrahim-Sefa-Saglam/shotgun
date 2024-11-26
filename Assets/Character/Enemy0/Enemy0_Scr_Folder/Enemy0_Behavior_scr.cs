using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_Behavior_scr : MonoBehaviour
{
   private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Color TookDamageColor;
    private string currentState;
    private string Idle_Anim = "Enemy0_Idle_anim";
    private string Move_anim = "Enemy0_Move_anim";
    private string Attack_Anim = "Enemy0_Attack_anim";
    private string TakeDmg_Anim = "Enemy0_TakeDmg_anim";

    private string Die_Anim = "Enemy0_Die_anim";
    public float moveSpeed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = Idle_Anim;    
    }

    void Update()
    {   
        // this mob will no be controlled by the player, these are temperory implementations to controll and test mechanics
        bool moveLeft = Input.GetKey("a"); 
        bool moveRight = Input.GetKey("d");
        Movement(moveLeft, moveRight);        
        Attack(Input.GetKeyDown(KeyCode.W));
        TakeDmg(Input.GetKeyDown(KeyCode.S));
        Die(Input.GetKeyDown(KeyCode.Space));
    }
    
    // ********** BEHAVIOR HANDLING  ********** // 
    void Movement(bool isLeftKey, bool isRightKey)
    {   
        Vector3 scale = transform.localScale;

        // if left input, go left
        if (isLeftKey){ 
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            scale.x = Mathf.Abs(scale.x); // Ensure facing left
            transform.localScale = scale;                
        }
        // if right input, go right    
        else if (isRightKey){
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            scale.x = -Mathf.Abs(scale.x); // Ensure facing right
            transform.localScale = scale;
        }        
        //if no direction, dont no velocity and load Idle animation and return
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 is the base layer index
            if (stateInfo.normalizedTime >= 1f && stateInfo.IsName(currentState)) ChangeAnimState(Idle_Anim); // Execute only after the current animation finishes
            return;
        }
        // load Move animation 
        ChangeAnimState(Move_anim);
    }
    void Attack(bool keyInput)
    {        
        if(!keyInput)return;
        ChangeAnimState(Attack_Anim);
        Invoke("ReturnToIdle", GetAnimationClipLength(Attack_Anim));
    }
    void TakeDmg(bool keyInput)
    {        
        if(!keyInput)return;
        ChangeAnimState(TakeDmg_Anim);
        if(TookDamageColor != null)spriteRenderer.color = TookDamageColor;
        Invoke("ReturnToIdle", GetAnimationClipLength(TakeDmg_Anim));

    }
   void Die(bool keyInput)
    {        
        if(!keyInput)return;
        ChangeAnimState(Die_Anim);
        Destroy(gameObject, GetAnimationClipLength(Die_Anim));        
    }

    // ********** ANIMATION HANDLING  ********** // 
   void ChangeAnimState(string newState)
{
    // Guards the current animation from itself
    if (currentState == newState) return;
    if (newState == Idle_Anim && (currentState == Attack_Anim || currentState == TakeDmg_Anim || currentState == Die_Anim)) return;
    if (newState == Move_anim && (currentState == Attack_Anim || currentState == TakeDmg_Anim || currentState == Die_Anim)) return;
    if (newState == Attack_Anim && (currentState == TakeDmg_Anim || currentState == Die_Anim)) return;
    if (newState == TakeDmg_Anim && currentState == Die_Anim) return;

    animator.Play(newState);
    currentState = newState;
}
    float GetAnimationClipLength(string clipName)
    {
    AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
    foreach (var clip in clips)
    {
        if (clip.name == clipName)
            return clip.length;
    }
    Debug.LogWarning($"Animation clip {clipName} not found!");
    return 0f;
    }
    void ReturnToIdle()
    {
    currentState = Idle_Anim;
    spriteRenderer.color = new Color(255f, 255f, 255f);
    animator.Play(Idle_Anim);

    }

}
