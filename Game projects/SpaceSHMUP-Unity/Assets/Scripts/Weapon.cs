/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : Weapon.cs
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

public enum eWeaponType {
    none,
    blaster,
    spread,
    phaser,
    missle,
    laser,
    shield
}
[System.Serializable]
public class WeaponDefinition{
    public eWeaponType type = eWeaponType.none; 
    [Tooltip("Letter to show on the PowerUp Cube")]
    public string letter;
    [Tooltip("Color of PowerUp Cube")]
    public Color powerUpColor = Color.white;

    [Tooltip("Prefab of Weapon model that is attached to the Player Ship")]
    public GameObject  weaponModelPrefab;

    [Tooltip("Prefab of projectile that is fired")]
    public GameObject projectilePrefab;
    [Tooltip("Color of the Projectile that is fired")]
    public Color projectileColor = Color.white;
    [Tooltip("Damage caused when a single Projectile hits an Enemy")]
    public float damageOnHit = 2;
    [Tooltip("Damage caused per second by the Laser [Not Implemented]")] 
    public float damagePerSec = 0;
    [Tooltip("Seconds to delay between shots")]
    public float delayBetweenShots = 0;
    [Tooltip("Velocity of individual Projectiles")]
    public float Velocity = 50;
}
public class Weapon : MonoBehaviour{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Dynamic")]
    [SerializeField]
    [Tooltip("Setting this manually while playing does not work properly.")]
    private eWeaponType _type = eWeaponType.none;
    private WeaponDefinition def;
    private float nextShotTime; // Time the Weapon will fire next

    private GameObject weaponModel;
    private Transform eWeaponAnchor;

    private Transform shotPointTrans;

    [Header("Inscribed")]
    public WeaponDefinition[] weaponDefinitions;  // b

    private BoundsCheck bndCheck;
void Start() {
        // Set up PROJECTILE_ANCHOR if it has not already been done
        if (PROJECTILE_ANCHOR == null) {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        shotPointTrans = transform.GetChild(0);

        // Call SetType() for the default _type set in the Inspector
        SetType( _type );

        // Find the fireDelegate of a Hero Component in the parent hierarchy
        Hero hero = GetComponentInParent<Hero>();
        if (hero != null) hero.fireEvent += Fire;
       
    }

    public eWeaponType type {
        get { return( _type ); }
        set { SetType( value ); }
    }

    public void SetType( eWeaponType wt ) {
        _type = wt;
        if (_type == eWeaponType.none) {
            this.gameObject.SetActive(false);
        } else {
            this.gameObject.SetActive(true);
        }

        // Get the WeaponDefinition for this type from Main
        def = Main.GET_WEAPON_DEFINITION(_type);
        // Destroy old model and then attach a model for this weapon
        if (weaponModel != null) Destroy(weaponModel);
        weaponModel = Instantiate<GameObject>(def.weaponModelPrefab, transform);
        weaponModel.transform.localPosition = Vector3.zero;

        nextShotTime = 0; // You can fire immediately after _type is set.
    }

    private void Fire() {
        Debug.Log("Fire M");
        // If this GameObject is inactive, return
        if (!gameObject.activeInHierarchy) return;

        // If it hasn't been enough time between shots, return
        if (Time.time < nextShotTime + def.delayBetweenShots) return;

        ProjectileHero p;
        Vector3 vel = Vector3.up * def.Velocity;

        switch (type) {
            case eWeaponType.blaster:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                break;

            case eWeaponType.spread:
                p = MakeProjectile();
                p.rigid.velocity = vel;

                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;

                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                break;
        }
    }

    private ProjectileHero MakeProjectile() {    
    GameObject go;
    go = Instantiate<GameObject>(def.projectilePrefab, PROJECTILE_ANCHOR);    
    ProjectileHero p = go.GetComponent<ProjectileHero>();

    Vector3 pos = shotPointTrans.position;
    pos.z = 0;    // o
    p.transform.position = pos;

    p.type = type;
    nextShotTime = Time.time + def.delayBetweenShots;    
    return( p );
}


}

