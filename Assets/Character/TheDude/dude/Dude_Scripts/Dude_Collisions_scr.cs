using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude_Collisions_scr : MonoBehaviour
{
    public GameObject pointerAxis;
    private Dude_gun_handler dude_Gun_Handler;
    void Start()
    {
        dude_Gun_Handler = pointerAxis.GetComponent<Dude_gun_handler>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
    
    if (collider.gameObject.CompareTag("gun"))
    {   
        dude_Gun_Handler.PickUpGun(collider.gameObject);
    }
    }

}
