using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_TakeDmg_scr : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private string Idle_Anim = "Enemy0_Idle_anim";
    private string TakeDmg_Anim = "Enemy0_TakeDmg_anim";
    public SpriteRenderer spriteRenderer;
    public Color TookDamageColor;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = "Enemy0_Idle_anim";   
    }

    // Update is called once per frame
    void Update()
    {
        TakeDmg(Input.GetKeyDown(KeyCode.S));
    }
    void TakeDmg(bool keyInput)
    {        
        if(!keyInput)return;
        ChangeAnimState(TakeDmg_Anim);
        Debug.Log( GetAnimationClipLength(TakeDmg_Anim));
        if(TookDamageColor != null)spriteRenderer.color = TookDamageColor;
        Invoke("On_TakeDmg_Animation_Complete", GetAnimationClipLength(TakeDmg_Anim));

    }
    void ChangeAnimState(string newState){
        // guards the current animation from itself
        if(currentState == newState) return;
        animator.Play(newState);

        currentState = newState;
    }

    void On_TakeDmg_Animation_Complete()
{   
    spriteRenderer.color = new Color(255f, 255f, 255f);
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

