using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_Die_scr : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private string Die_Anim = "Enemy0_Die_anim";
    

    void Start()
    {
        animator = GetComponent<Animator>(); 
        currentState = "Enemy0_Idle_anim";
    }

    // Update is called once per frame
    void Update()
    {
        Die(Input.GetKeyDown(KeyCode.Space));
    }
    void Die(bool keyInput)
    {        
        if(!keyInput)return;
        ChangeAnimState(Die_Anim);
        Debug.Log( GetAnimationClipLength(Die_Anim));
        Destroy(gameObject, GetAnimationClipLength(Die_Anim));        
    }
    void ChangeAnimState(string newState){
        // guards the current animation from itself
        if(currentState == newState) return;
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
}
