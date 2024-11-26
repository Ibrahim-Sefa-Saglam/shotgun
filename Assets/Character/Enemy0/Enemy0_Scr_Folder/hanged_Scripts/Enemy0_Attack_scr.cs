using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_scr : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private string Idle_Anim = "Enemy0_Idle_anim";
    private string Attack_Anim = "Enemy0_Attack_anim";
    
    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = "Enemy0_Idle_anim";
    }
    void Update()
    {
        Attack(Input.GetKeyDown(KeyCode.W));
    }
    void Attack(bool keyInput)
    {        
        if(!keyInput){
            return;
        }
        ChangeAnimState(Attack_Anim);
        Debug.Log( GetAnimationClipLength(Attack_Anim));
        Invoke("OnAttackAnimationComplete", GetAnimationClipLength(Attack_Anim));

    }

    void ChangeAnimState(string newState){
        // guards the current animation from itself
        if(currentState == newState) return;
        animator.Play(newState);

        currentState = newState;
    }

    void OnAttackAnimationComplete()
{
    ChangeAnimState(Idle_Anim);
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
}