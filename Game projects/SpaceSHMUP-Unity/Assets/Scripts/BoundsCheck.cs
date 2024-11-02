/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Space SHMUP Exercise
* FILE NAME       : BoundsCheck.cs
* DESCRIPTION     : Keep game object on screen
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/30	Akram Taghavi-Burris    Initial Setup
* 
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    // Enum to define different types of bounds checking: center, inset, and outset.
    /** NOTE: An enum is a data type that represents a collection of named constants, allowing you to refer to them by name instead of numeric values. 
    Learn more https://youtu.be/L2E2aB1CMYw?si=1gtf1uST0D8RyGym **/
    public enum eType { center, inset, outset };
    [System.Flags]
    public enum eScreenLocs{
        onScreen =0, // 0000 in binary
        offRight = 1,// 0001 in binary
        offLeft = 2, // 0010 in binary
        offUp = 4,   // 0100 in binary
        offDown = 8, // 1000 in binary
    }

    [Header("Inscribed")]
    public eType boundsType = eType.center; // Type of bounds checking (center, inset, or outset)
    public float radius = 1f; // Radius used for bounds checking
    public bool keepOnScreen = true; //Ensure the game object stays on screen

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen; // Screen location
    //public bool isOnScreen = true; //is the game object on screen
    public float camWidth;  // Width of the camera view in world units
    public float camHeight; // Height of the camera view in world units

    // Awake is called once at instantiation
    void Awake()
    {
        camHeight = Camera.main.orthographicSize; // Get the camera's orthographic size (height)
        camWidth = camHeight * Camera.main.aspect; // Calculate width based on aspect ratio
    }//end Awake

    // LateUpdate is called after all other Update methods
    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset
        /** NOTE: The checkRadius variable helps define how far from the actual screen bounds (determined by camWidth and camHeight) the game object can be positioned based on its type of bounds checking (inset, outset, or center).**/

        float checkRadius = 0; // Initialize the check radius
        if (boundsType == eType.inset) checkRadius = -radius; // Set checkRadius for inset type
        if (boundsType == eType.outset) checkRadius = radius; // Set checkRadius for outset type

        Vector3 pos = transform.position; // Store the current position of the object
        screenLocs = eScreenLocs.onScreen;
        //isOnScreen = true; //Game object is on screen

        /** Restrict the X position to camWidth **/

        // Check if outside the right boundary
        if (pos.x > camWidth + checkRadius)
        { 
            pos.x = camWidth + checkRadius; // Clamp the X position to the right boundary
            screenLocs |= eScreenLocs.offRight;
            //isOnScreen = false; //Game object is off screen

        }//end if (pos.x > camWidth + checkRadius)

        // Check if outside the left boundary 
        if (pos.x < -camWidth - checkRadius)
        { 
            pos.x = -camWidth - checkRadius; // Clamp the X position to the left boundary
            screenLocs = eScreenLocs.offLeft;
            //isOnScreen = false; //Game object is off screen

        }//end if (pos.x < -camWidth - checkRadius)

        /** Restrict the Y position to camHeight**/

        // Check if outside the upper boundary
        if (pos.y > camHeight + checkRadius)
        { 
            pos.y = camHeight + checkRadius; // Clamp the Y position to the upper boundary
            screenLocs = eScreenLocs.offUp;
            //isOnScreen = false; //Game object is off screen

        }//end f (pos.y > camHeight + checkRadius)

        // Check if outside the lower boundary
        if (pos.y < -camHeight - checkRadius)
        {  
            pos.y = -camHeight - checkRadius; // Clamp the Y position to the lower boundary
            screenLocs = eScreenLocs.offDown;
            //isOnScreen = false; //Game object is on screen

        }//end if (pos.y < -camHeight - checkRadius)

        //Check if game object is off screen and is supposed to be kept on screen
        if (keepOnScreen && !isOnScreen)
        {  
            transform.position = pos; //update position 
            screenLocs = eScreenLocs.onScreen;
            //isOnScreen = true; //Game object is on screen
        }//end if (keepOnScreen && !isOnScreen)
        else
        {
            transform.position = pos; // Update the object's position to the clamped value
        }


    }//end LateUpdate()

    public bool isOnScreen{
        get { return screenLocs == eScreenLocs.onScreen; }
    }

    public bool LocIs(eScreenLocs checkLoc){
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkLoc) == checkLoc);
    }

}
