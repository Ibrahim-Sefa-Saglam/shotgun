using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickUp_Scr : MonoBehaviour
{
    public GameObject gunObject;    // Public variable for assigning the gun object
    public GameObject pointerObject;  // Public variable for assigning the pointer object

    // Update is called once per frame
    void Update()
    {
        if (gunObject != null && pointerObject != null)
        {
            // Match the position of the gun to the pointer
            gunObject.transform.position = pointerObject.transform.position;

            // Match the rotation of the gun to the pointer
            gunObject.transform.rotation = pointerObject.transform.rotation;
        }
    }
}
