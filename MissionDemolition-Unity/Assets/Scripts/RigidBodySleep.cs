/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Misson Demolition
* FILE NAME       : RigidBodySleep.cs
* DESCRIPTION     : Short Description of script.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/07	Jayden Younger   		 Stops any phsyical behaviors
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodySleep : MonoBehaviour{
    private int sleepCountdown = 4;
    private Rigidbody rigid;

    // Awake is called once at instantiation
    void Awake(){
        rigid = GetComponent<Rigidbody>();  
    }

    void FixedUpdate(){
        if ( sleepCountdown > 0)
        {
            rigid.Sleep();
            sleepCountdown--;
        }
    }
}
