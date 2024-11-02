/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Space SHMUP Exercise
* FILE NAME       : Hero.cs
* DESCRIPTION     : Hero Ship behaviors
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/30	Akram Taghavi-Burris    Initial Setup
* 
*
/******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S { get; private set; }  // Singleton property , only one instance

    [Header("Inscribed")]
    // These fields control the movement of the ship
    [Tooltip("Ship acceleration")]
    public float speed = 30;

    // Roll Multiplier: Controls the tilt from side to side (like rolling a log)
    [Tooltip("Side-to-side tilt. Higher values, quicker rolls")]
    public float rollMult = -45;

    // Pitch Multiplier: Controls the lift or lowering the nose (like nodding up and down).
    [Tooltip("Nose up or down movement. Higher values, sharper climbs/descents")]
    public float pitchMult = 30;

    public GameObject projectilePrefab;

    public float projectileSpeed = 40;

    public Weapon[] weapons;

    [Header("Dynamic")]
    [Range(0, 4)]
    [SerializeField]
    private float _shieldLevel = 1;
    //public float shieldLevel = 1;

    [Tooltip("This field holds a referance to the last triggering GameObject. ")]
    private GameObject lastTriggerGo = null;

    public delegate void WeaponFireDelegate();
    public event WeaponFireDelegate fireEvent;

    // Awake is called once at instantiation
    void Awake()
    {
        //Singleton pattern requires a check if other instances exist 
        if (S == null)
        {
            //Make this the single instance, if none exist
            S = this;
        }
        else
        {
            //Otherwise send an error
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");

            /** NOTE: Proper singleton implementation would require that the game object destroy itself if one already exists**/
        }//end if(S == null)

        //fireEvent += TempFire;
        //ClearWeapons();
        //weapons[0].SetType(eWeaponType.blaster);

    }//end Awake()

    // Start is called before the first frame update
    void Start()
    {

    }//end Start()

    // Update is called once per frame
    void Update()
    {

        // Check for Input for Get Axis
        // Learn more https://youtu.be/MK4OmsViqMA?si=B8JHJhcv3LcZeD-I
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic                      
        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);
        /** NOTE: Quaternion.Euler() is a method in Unity used to create a quaternion  from Euler angles (pitch, yaw, roll). Learn more: https://youtu.be/hd1QzLf4ZH8?si=MFBJsWkPBVnK7oVl
        **/

        //if (Input.GetKeyDown (KeyCode.Space)){
           // TempFire();
        //}
        if (Input.GetAxis("Jump") > 0){
            //Debug.Log("Fire");
            if (fireEvent != null)
            {
                fireEvent();

            }
            
        }
    }//end Update()

/*
    void TempFire(){
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        //rigidB.velocity = Vector3.up * projectileSpeed;

        ProjectileHero proj = projGO.GetComponent<ProjectileHero>();
        proj.type = eWeaponType.blaster;
        float tSpeed = Main.GET_WEAPON_DEFINITION(proj.type).Velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }
*/

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        // Debug.Log("Shield trigger hit by: " +go.gameObject.name);
        if (go == lastTriggerGo) return;

        lastTriggerGo = go;

        Enemy enemy = go.GetComponent<Enemy>();
        PowerUp pUp = go.GetComponent<PowerUp>();

        if (enemy != null)
        {
            shieldLevel--;
            Destroy(go);
        } else if(pUp != null){
            AbsorbPowerUp(pUp);
        }else {
            Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
        }
    }

    public void AbsorbPowerUp(PowerUp pUp){
        Debug.Log("Absorbed PowerUp: " + pUp.type);
        switch (pUp.type){
            case eWeaponType.shield:
                    shieldLevel++;
                break;
            default:
                if (pUp.type == weapons[0].type)
                {
                    Weapon weapon = GetEmptyWeaponSlot();
                    if(weapon != null)
                    {
                        weapon.SetType(pUp.type);
                    } else
                    {
                        ClearWeapons();
                        weapons[0].SetType(pUp.type);
                    }
                }
                break;
        }
        pUp.AbsorbedBy(this.gameObject);
    }

    public float shieldLevel{
        get { return (_shieldLevel); }

        private set
        {
            _shieldLevel = Mathf.Min(value, 4);

            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.HERO_DIED();
            }
        }
    }

    Weapon GetEmptyWeaponSlot(){
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == eWeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(eWeaponType.none);
        }
    }
}
