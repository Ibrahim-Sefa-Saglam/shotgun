using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_Die_scr : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Die(Input.GetKeyDown(KeyCode.Space));
    }
    void Die(bool keyInput)
    {        
        if(!keyInput)return;
        Debug.Log("Died" );        
    }

}
