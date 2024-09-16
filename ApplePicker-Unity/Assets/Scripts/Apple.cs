/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Apple Picker
* FILE NAME       : Apple.cs
* DESCRIPTION     : Condiions for the apple prefab
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/10	Jayden Younger    		Added destroy apple when it goes to far
* 2024/09/15    Jayden Younger          Notify when apple missed the basket
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour{
    public static float bottomY = -20;

    // Update is called once per frame
    void Update(){
        // If apple goes past the bottom Y position threshold, destrot the game object
        if(transform.position.y < bottomY){
            Destroy(this.gameObject);

            // Call that apple was missed
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleMissed();
        }
    }
}
