using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunMovement_Scr : MonoBehaviour
{
    public GameObject gunObject;    // Public variable for assigning the gun object
    public GameObject pointerObject;  // Public variable for assigning the pointer object
    private Transform gunDirection; // Reference to the direction object inside the gunObject

    public GameObject physicalCont;   // Public variable for assigning the physicalCont object
    public GameObject physicalDir;    // Public variable for assigning the physicalDir object

    private void Start()
    {
        if (gunObject != null)
        {
            // Get the "direction" object inside the gunObject
            gunDirection = gunObject.transform.Find("direction");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (physicalCont != null && physicalDir != null && pointerObject != null && gunObject != null)
        {   
            // Match the position of the gun to the pointer
            gunObject.transform.position = pointerObject.transform.position;

            // Match the rotation of the gun to the pointer
            gunObject.transform.rotation = pointerObject.transform.rotation;

            // call the alingment
            AlignPhysical();
        }

    }

 void Align_GunDirection_WithPointer()
{
    // Get horizontal position relative to gunObject and player body
    float directionHorizontalPos = gunDirection.position.x - gunObject.transform.position.x;
    float pointerHorizontalPos = pointerObject.transform.position.x - transform.position.x;

    // Check if horizontal directions differ
    if (Mathf.Sign(directionHorizontalPos) != Mathf.Sign(pointerHorizontalPos))
    {
        // Flip the gun horizontally by adjusting the local scale's X component
        Vector3 gunScale = gunObject.transform.localScale;
        gunScale.x *= -1;
        gunObject.transform.localScale = gunScale;
    }

    // Check if the gun is upside down (local up direction pointing downward in world space)
    if (Vector3.Dot(gunObject.transform.up, Vector3.up) < 0)
    {
        // Rotate the gun 180 degrees around the local X-axis to correct the up direction
        gunObject.transform.Rotate(180f, 0f, 0f, Space.Self);
    }
}    void AlignPhysical()
    {
        // Get horizontal position relative to physicalCont and player body
        float physicalDirHorizontalPos = physicalDir.transform.position.x - physicalCont.transform.position.x;
        float pointerHorizontalPos = pointerObject.transform.position.x - transform.position.x;

        // Check if horizontal directions differ
        if (Mathf.Sign(physicalDirHorizontalPos) != Mathf.Sign(pointerHorizontalPos))
        {
            // Flip the physicalCont horizontally by adjusting the local scale's X component
            Vector3 physicalScale = physicalCont.transform.localScale;
            physicalScale.x *= -1;
            physicalCont.transform.localScale = physicalScale;
        }
    
        Align_GunDirection_WithPointer();
    }
}
