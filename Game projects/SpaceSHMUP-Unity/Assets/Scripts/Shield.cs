/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Space SHMUP Exercise
* FILE NAME       : Sheild.cs
* DESCRIPTION     : Shield behaviors
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/30	Akram Taghavi-Burris    Initial Setup
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotationsPerSecond = 0.1f; // Speed of rotation in degrees per second

    [Header("Dynamic")]
    public int levelShown = 0; // Current shield level displayed

    // Variables with no public identifier will be set to "private" automatically and will not  appear in the Inspector
    Material mat; // Material reference to the shield's texture

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material; // Get the material of the shield's renderer 
    }//end Start()

    void Update()
    {
        // Read the current shield level from the Hero Singleton
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);

        // If this is different from levelShown.
        if (levelShown != currLevel)
        {
            levelShown = currLevel;

            // Adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }//end if(levelShown != currLevel)

        // Rotate the shield a bit every frame based on elapsed time and rotations per second
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f; 
        transform.rotation = Quaternion.Euler(0, 0, rZ); // Set the rotation of the shield
    }//end Update()



}
