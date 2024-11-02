/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : PowerUp.cs
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

[RequireComponent(typeof(BoundsCheck))]
[RequireComponent(typeof(Rigidbody))] // Ensure Rigidbody component is present
public class PowerUp : MonoBehaviour {

    [Header("Inscribed")]
    [Tooltip("x holds a min value and y a max value for a Random.Range() call.")]
    public Vector2 rotMinMax = new Vector2(15, 90);

    [Tooltip("x holds a min value and y a max value for a Random.Range() call.")]
    public Vector2 driftMinMax = new Vector2(.25f, 2);

    public float lifeTime = 10f; // PowerUp will exist for this many seconds
    public float fadeTime = 4f;  // Fade out over this many seconds

    [Header("Dynamic")]
    public eWeaponType type;     // Type of the PowerUp
    public GameObject cube;      // Reference to the PowerCube child
    public TextMesh letter;      // Reference to the TextMesh
    public Vector3 rotPerSecond; // Euler rotation speed for PowerCube
    private float birthTime;     // The time when this PowerUp is instantiated

    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Material cubeMat;

    void Awake() {
        // Find the Cube reference (assuming there's only a single child)
        cube = transform.GetChild(0).gameObject;

        // Get TextMesh component from the child object
        letter = GetComponentInChildren<TextMesh>();

        // Get the Rigidbody and BoundsCheck components
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();

        // Get the material of the cube (assuming Renderer is on the same object)
        cubeMat = cube.GetComponent<Renderer>().material;

        // Set a random velocity in the XY plane
        Vector3 vel = Random.onUnitSphere;
        vel.z = 0; // Flatten the velocity to the XY plane
        vel.Normalize();
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;

        // Set the initial rotation to no rotation
        transform.rotation = Quaternion.identity;

        // Randomize the rotation speed for the PowerCube using rotMinMax values
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
                                   Random.Range(rotMinMax.x, rotMinMax.y),
                                   Random.Range(rotMinMax.x, rotMinMax.y));

        birthTime = Time.time; // Store the birth time
    }

    void Update() {
        // Rotate the PowerCube
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        // Calculate the fade-out effect based on lifetime and fade time
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;

        if (u > 1) {
            Destroy(this.gameObject); // Destroy if fade is complete
            return;
        }

        if (u < 0) {
            // Fade out the cube and the letter (change opacity)
            Color cubeColor = cubeMat.color;
            cubeColor.a = 1 - u;
            cubeMat.color = cubeColor;

            Color letterColor = letter.color;
            letterColor.a = 1 - (u * 0.5f); // Fade the letter slower
            letter.color = letterColor;
        }

        // Destroy the object if it's off-screen
        if (!bndCheck.isOnScreen) {
            Destroy(gameObject);
        }
    }

    // Getter and Setter for 'type'
    private eWeaponType _type;
    public eWeaponType Type {
        get { return _type; }
        set { SetType(value); }
    }

    public void SetType(eWeaponType wt) {
        // Grab the WeaponDefinition from Main
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(wt);

        // Set the PowerCube color and letter based on the weapon definition
        cubeMat.color = def.powerUpColor; // Set cube color
        letter.text = def.letter;         // Set the letter text
        _type = wt;                       // Set the internal type
    }

    /// <summary>
    /// This function is called by the Hero class when a PowerUp is collected.
    /// </summary>
    /// <param name="target">The GameObject absorbing this PowerUp</param>
    public void AbsorbedBy(GameObject target) {
        // When absorbed by the player or object, destroy this PowerUp
        Destroy(this.gameObject);
    }
}
