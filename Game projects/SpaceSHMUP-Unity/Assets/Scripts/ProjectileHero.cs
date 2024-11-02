/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : ProjectileHero.cs
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

[RequireComponent( typeof(BoundsCheck) )]
public class ProjectileHero : MonoBehaviour {
    private BoundsCheck bndCheck;
    private Renderer rend;

    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField]
    private eWeaponType _type;

    // This public property masks the private field _type
    public eWeaponType type {
        get { return( _type ); }
        set { SetType( value ); }
    }

    void Awake () {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update () {
        if ( bndCheck.LocIs( BoundsCheck.eScreenLocs.offUp ) ) {
            Destroy( gameObject );
        }
    }

    /// <summary>
    /// Sets the _type private field and colors this projectile to match the WeaponDefinition.
    /// </summary>
    /// <param name="eType">The eWeaponType to use.</param>
    public void SetType( eWeaponType eType ) {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION( _type );
        rend.material.color = def.projectileColor;
    }

    /// <summary>
    /// Allows Weapon to easily set the velocity of this ProjectileHero.
    /// </summary>
    public Vector3 vel {
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }

    void OnCollisionEnter(Collision coll) {
        Destroy(this.gameObject);
    }

}
