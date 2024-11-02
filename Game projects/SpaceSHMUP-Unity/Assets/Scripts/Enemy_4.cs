/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Space SHMUP Exercise
* FILE NAME       : Enemy_0.cs
* DESCRIPTION     : Short Description of script.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2000/01/01		Developer's Name    		 Created <short comment of changes>
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyShield))]
public class Enemy_4 : Enemy
{
    [Header("Enemy_4 Inscribed Fields")]
    public float duration = 4f;  // Duration of interpolation movement

    private EnemyShield[] allShields;
    private EnemyShield thisShield;
    private Vector3 p0, p1;  // The two points to interpolate between
    private float timeStart; // Time at which movement started

    void Start()
    {
        // Get all shields attached to this enemy
        allShields = GetComponentsInChildren<EnemyShield>();
        thisShield = GetComponent<EnemyShield>();

        // Initially set p0 & p1 to the current position (from Main.SpawnEnemy())
        p0 = p1 = pos;
        InitMovement();
    }

    void InitMovement()
    {
        // Set p0 to the old p1
        p0 = p1;

        // Assign a new on-screen location to p1
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        // Ensure that p1 is in a different quadrant than p0
        if (p0.x * p1.x > 0 && p0.y * p1.y > 0)
        {
            if (Mathf.Abs(p0.x) > Mathf.Abs(p0.y))
            {
                p1.x *= -1;
            }
            else
            {
                p1.y *= -1;
            }
        }

        // Reset the time for the new movement
        timeStart = Time.time;
    }

    public override void Move()
    {
        // Interpolation value u ranges from 0 to 1 over 'duration'
        float u = (Time.time - timeStart) / duration;

        if (u >= 1)
        {
            InitMovement();  // Start a new movement cycle
            u = 0;
        }

        // Adjust u to create a smooth easing effect
        u = u - 0.15f * Mathf.Sin(u * 2 * Mathf.PI);

        // Move the Enemy_4 between points p0 and p1
        pos = (1 - u) * p0 + u * p1;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;

        // Check if this collision is with a ProjectileHero
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if (p != null)
        {
            // Destroy the ProjectileHero regardless of whether this enemy is on-screen
            Destroy(otherGO);

            // Only damage the Enemy if it's on screen
            if (bndCheck.isOnScreen)
            {
                // Find the GameObject that was hit
                GameObject hitGO = coll.contacts[0].thisCollider.gameObject;
                if (hitGO == otherGO)
                {
                    hitGO = coll.contacts[0].otherCollider.gameObject;
                }

                // Get the damage amount from the weapon definition
                float dmg = Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;

                // Find the shield that was hit (if any)
                bool shieldFound = false;
                foreach (EnemyShield es in allShields)
                {
                    if (es.gameObject == hitGO)
                    {
                        es.TakeDamage(dmg);
                        shieldFound = true;
                        break;
                    }
                }

                // If no shield was hit, apply damage to this enemy
                if (!shieldFound)
                {
                    thisShield.TakeDamage(dmg);
                }

                // If thisShield is still active, stop further processing
                if (thisShield.isActive) return;

                // If the shield is destroyed, report it to Main and destroy the ship
                if (!calledShipDestroyed)
                {
                    Main.SHIP_DESTROYED(this);
                    calledShipDestroyed = true;
                }

                // Destroy this Enemy_4 object
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Enemy_4 hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}