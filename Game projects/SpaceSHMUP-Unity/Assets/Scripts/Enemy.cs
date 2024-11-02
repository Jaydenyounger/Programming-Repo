/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Space SHMUP Exercise
* FILE NAME       : Enemy.cs
* DESCRIPTION     : Enemy Behavior
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

/**NOTE:RequireComponent is an attribute that enforces the presence of a specified type of component on the GameObject, ensuring that the required component is automatically added for the script to function correctly. Learn more https://youtu.be/X7bioowXJiU?si=dGYpBLQurMpTkxuy
 **/ 
[RequireComponent(typeof(BoundsCheck))]
public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;   // The movement speed is 10m/s
    public float fireRate = 0.3f;  // Seconds per shot (Unused)
    public float health = 10;    // Damage needed to destroy this enemy
    public int score = 100;      // Points earned for destroying this enemy

    public float powerUpDropChance = 1f;

    protected bool calledShipDestroyed = false;

    //Reference to BoundsCheck component
    protected BoundsCheck bndCheck;


    /** NOTE: A property is a method that provides a way to get or set the value of a field, allowing for additional logic during access; a field, on the other hand, is a variable that stores data directly. Learn more https://youtu.be/HzIqrlSbjjU?si=_ztRLrNhtBG2O2g6
    **/
public Vector3 pos
    {  // Property to get and set the enemy's position
        get { return this.transform.position; }  // Return the current position of the enemy
        set { this.transform.position = value; } // Set the enemy's position to the specified value
    }


    // Awake is called once at instantiation
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>(); //Get the BoundsCheck component 
    }//end Awake

    // Update is called once per frame
    void Update()
    {
        Move();  // Call the Move method every frame
        //Debug.Log(bndCheck.isOnScreen);
        // Check whether this Enemy has gone off the bottom of the screen
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)){
            Destroy(gameObject);
        }

    }//end Update


    /** NOTE: A virtual method is a method defined in a base class that can be overridden in derived classes, allowing for polymorphic behavior and customization of functionality in subclasses.
      Learn more https://youtu.be/h0J4gs4DW5A?si=C6JbZCT5xL-PCOe0
    **/

    // Method to move the enemy
    public virtual void Move()
    {  
        Vector3 tempPos = pos;  // Store the current position in a temporary variable
        tempPos.y -= speed * Time.deltaTime;  // Move the enemy downwards based on speed and frame time
        pos = tempPos;  // Update the enemy's position with the new value
    
    }//end Move() 

    // Start is called before the first frame update
    void Start()
    {

    }//end Start()
/*
    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;

        if(otherGO.GetComponent<ProjectileHero>() != null) { 
            Destroy(otherGO);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enemy hit by no-ProjectileHero: " + otherGO.name);
        }
    }  
*/
    void OnCollisionEnter(Collision coll) {
    GameObject otherGO = coll.gameObject;

    // Check for collisions with ProjectileHero
    ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
    if (p != null) {
        // Only damage this Enemy if it's on screen
        if (bndCheck.isOnScreen) {
            // Get the damage amount from the Main WEAP_DICT.
            health -= Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;
            if (health <= 0) {
                // Tell Main that this ship was destroyed
                if (!calledShipDestroyed) {
                    calledShipDestroyed = true;
                    Main.SHIP_DESTROYED(this);
                }
                // Destroy this Enemy
                Destroy(this.gameObject);
                Destroy(p);
            }
        }
        // Destroy the ProjectileHero regardless
        Destroy(otherGO);
    } else {
        print("Enemy hit by non-ProjectileHero: " + otherGO.name);
    }
}
 }

