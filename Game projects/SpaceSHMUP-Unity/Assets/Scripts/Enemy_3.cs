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

public class Enemy_3 : Enemy {           // Enemy_3 also extends the Enemy class    // a

    [Header("Enemy_3 Inscribed Fields")]
    public float lifeTime = 5;

    public Vector2 midPointYRange = new Vector2(1.5f, 3); 
    [Tooltip("If true, the Bezier points a path are drawn in the Scene pane.")]
    public bool drawDebugInfo = true;

    [Header("Enemy_3 Private Fields")]
    private Vector3[] points; // The three points for the Bezier curve
    [SerializeField] private float birthTime;

    void Start() {  // Again, Start works because it is not used in the Enemy superclass
        points = new Vector3[3]; // Initialize points

        // The start position has already been set by Main.SpawnEnemy()
        points[0] = pos;

        // Set xMin and xMax the same way that Main.SpawnEnemy() does
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        // Pick a random middle position in the bottom half of the screen
        Vector3 mid = Vector3.zero;
        mid.x = Random.Range(xMin, xMax);
        mid.y = Random.Range(midPointYRange.x, midPointYRange.y);
        points[1] = mid;

        // Pick a random final position above the top of the screen
        points[2] = Vector3.zero;
        points[2].y = bndCheck.camHeight + bndCheck.radius;
        points[2].x = Random.Range(xMin, xMax);

        // Set the birthTime to the current time
        birthTime = Time.time;

        if (drawDebugInfo) DrawDebug();
    }

    public override void Move() {
        // Bezier curves work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1) {    // This Enemy_3 has finished its life
            Destroy(this.gameObject);
            return;
        }

        transform.rotation = Quaternion.Euler( u * 180, 0, 0 );

        // Interpolate the three Bezier curve points
        u = u - 0.1f * Mathf.Sin(u * Mathf.PI * 2);
        pos = Utils.Bezier(u, points);

        // Enemy_3 does not call base.Move()
    }

    void DrawDebug() {
        // Draw the three points
        Debug.DrawLine(points[0], points[1], Color.cyan, lifeTime);
        Debug.DrawLine(points[1], points[2], Color.yellow, lifeTime);

        // Draw the Bezier Curve
        Vector3 prevPoint = points[0];
        int numSections = 20;
        for (int i = 1; i <= numSections; i++) {
            float t = i / (float)numSections;
            Vector3 pt = Utils.Bezier(t, points);
            Color col = Color.Lerp(Color.cyan, Color.yellow, t);
            Debug.DrawLine(prevPoint, pt, col, lifeTime);
            prevPoint = pt;
        }
    }
}

