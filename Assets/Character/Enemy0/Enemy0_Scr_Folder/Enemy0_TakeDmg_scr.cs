using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0_TakeDmg_scr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeDmg(Input.GetKeyDown(KeyCode.S));
    }
    void TakeDmg(bool keyInput)
    {        
        if(!keyInput)return;
        Debug.Log("Took Damage");
    }

}
