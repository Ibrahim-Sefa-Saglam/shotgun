using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunMovement_Scr : MonoBehaviour
{
    public GameObject gunObject;    // Public variable for assigning the gun object, for Align_GunDirection_WithPointer
    public GameObject pointerObject;  // Public variable for assigning the pointer object, for all
    private Transform gunDirection; // Reference to the direction object inside the gunObject, for Align_GunDirection_WithPointer

    public GameObject physicalCont;   // Public variable for assigning the physicalCont object, for AlingPhysical
    public GameObject physicalDir;    // Public variable for assigning the physicalDir object, for AlingPhysical

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
        if (physicalCont != null && physicalDir != null && pointerObject != null)
        {   
            // Call the alignment
            AlignPhysical();
        }
        if(gunObject != null && pointerObject != null)
        {
            // Match the position of the gun to the pointer
            gunObject.transform.position = pointerObject.transform.position;

            // Match the rotation of the gun to the pointer
            gunObject.transform.rotation = pointerObject.transform.rotation;

            Align_GunDirection_WithPointer();
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
    }

    void AlignPhysical()
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
    }

    // Trigger detection method to check if player enters a trigger with a gun
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object the player collided with has the tag "gun"
        if (other.CompareTag("gunLayer"))
        {
            // Check if gunObject is null before assigning
            if (gunObject == null)
            {
                // Assign the collided object to gunObject
                gunObject = other.gameObject;

                // Get the "direction" object inside the newly assigned gunObject
                gunDirection = gunObject.transform.Find("direction");
                
                gunObject.layer = LayerMask.NameToLayer("eq_gun");
            }
        }
    }
}
