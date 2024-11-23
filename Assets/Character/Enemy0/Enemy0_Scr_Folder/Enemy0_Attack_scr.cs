using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_scr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack(Input.GetKeyDown(KeyCode.W));
    }
    void Attack(bool keyInput)
    {        
        if(!keyInput)return;
        Debug.Log("Attack");
    }
}

        