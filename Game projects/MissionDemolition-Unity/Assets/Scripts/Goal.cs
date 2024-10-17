/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Misson Demolition
* FILE NAME       : gOAL.cs
* DESCRIPTION     : set a target for the player to hit to go to the next level
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/7 	Jayden Younger  	    player hits goal
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour{

    static public bool goalMet = false;

    private void OnTriggerEnter(Collider other){
        Projectile proj = other.GetComponent<Projectile>();

        if (proj != null){
            Goal.goalMet = true;

            Material mat = GetComponent<Renderer>().material;

            Color c = mat.color;
            c.a = 0f;
            mat.color = c;
        }
    }
}
