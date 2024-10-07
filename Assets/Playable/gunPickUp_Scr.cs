using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickUp_Scr : MonoBehaviour
{
    public GameObject gunObject;    // Public variable for assigning the gun object
    public GameObject pointerObject;  // Public variable for assigning the pointer object
    private Transform gunDirection; // Reference to the direction object inside the gunObject

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
        if (gunObject != null && pointerObject != null  )
        {
            // Match the position of the gun to the pointer
            gunObject.transform.position = pointerObject.transform.position;

            // Match the rotation of the gun to the pointer
            gunObject.transform.rotation = pointerObject.transform.rotation;

            // Keep the direction of the gun aligned with the pointer's horizontal direction
            AlignDirectionWithPointer();
        }
    }

    void AlignDirectionWithPointer()
    {
        // Get horizontal position relative to gunObject and player body
        float directionHorizontalPos = gunDirection.position.x - gunObject.transform.position.x;
        float pointerHorizontalPos = pointerObject.transform.position.x -  transform.position.x;

        // Check if horizontal directions differ
        if (Mathf.Sign(directionHorizontalPos) != Mathf.Sign(pointerHorizontalPos))
        {
            // Flip the gun horizontally by adjusting the local scale's X component
            Vector3 gunScale = gunObject.transform.localScale;
            gunScale.x *= -1;
            gunObject.transform.localScale = gunScale;
        }
    }
}
