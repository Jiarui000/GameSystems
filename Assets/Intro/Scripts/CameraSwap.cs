using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// No need for UnityEngine.UI as we're hardcoding in OnGUI. UnityEngine.UI is for canvas GUI stuff

public class CameraSwap : MonoBehaviour
{
    public Transform[] lookObjects; // Array of objects to look at
    public bool smooth = true; // Is smooth enabled?
    public float damping = 6; // Smoothness value of camera
    [Header("GUI")]
    public float scrW;
    public float scrH;

    private int camIndex; // Index into array of lookObjects
    private int camMax; // Stores the total amount of lookObjects
    private Transform target; // Current target look object

    // Use this for initialization
    void Start()
    {
        // Last index of array
        camMax = lookObjects.Length - 1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Get current object to look at
        target = lookObjects[camIndex];
        // If target is not null
        if (target)
        {
            // Is smoothing enabled?
            if (smooth)
            {
                // Calculate direction to look at target
                Vector3 lookDirection = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                // Look at and dampen the rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                // just look at the target without the smooth and dampen
                transform.LookAt(target);
            }
        }
        else
        {
            // keep swapping cameras until a valid target is found
            CamSwap();
        }
    }

    void CamSwap()
    {
        // Increase index by 1 to select next object 

        camIndex++;

        // If index is greater than our max array size 
        if (camIndex > camMax)
        {
            // Reset camIndex back to zero
            camIndex = 0;
        }
    }
    private void OnGUI() // for a specific button
    {
        if (scrW != Screen.width / 16 || scrH != Screen.height / 9)
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 9;
        }
        if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrH, 1.5f * scrW, 0.75f * scrH), "Swap"))
        {
            // swap the cameras
            CamSwap();
        }
    }
}
