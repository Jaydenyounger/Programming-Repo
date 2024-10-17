/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Misson Demolition
* FILE NAME       : Projectile.cs
* DESCRIPTION     : throw a projectile
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
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour{
    private const float dim_mult = 0.55f;

    const int LOOKBACK_COUNT = 10;

    static List<Projectile> PROJECTILES = new List<Projectile>();

    [SerializeField]
    private bool _awake = true;
    
    public bool Awake { 
        get { return _awake; } 
        private set { _awake = value; }
    }

    private Vector3 prevPos;

    private List<float> deltas = new List<float>();
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start(){
        rigid = GetComponent<Rigidbody>();
        Awake = true;
        prevPos = new Vector3(1000, 1000, 0);

        deltas.Add(1000);
        PROJECTILES.Add(this);
    }

    void FixedUpdate() {
        if(rigid.isKinematic || !Awake) return;

        Vector3 deltaV3 = transform.position - prevPos;

        deltas.Add(deltaV3.magnitude);
        prevPos = transform.position;

        while(deltas.Count > LOOKBACK_COUNT){
            deltas.RemoveAt(0);
        }

        float MaxDelta = 0;
        foreach(float f in deltas){
            if (f > MaxDelta) MaxDelta = f;
        }

        if(MaxDelta <= Physics.sleepThreshold)
        {
            Awake = false;
            rigid.Sleep();
        }
    }

    private void OnDestroy() {
        PROJECTILES.Remove(this);
    }
    static public void DESTROY_PROJECTILES() {
        foreach (Projectile p in PROJECTILES)
        {
            Destroy(p.gameObject);
        }
    }
}
