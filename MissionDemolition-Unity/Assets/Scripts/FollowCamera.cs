/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Misson Demolition
* FILE NAME       : FollowCamera.cs
* DESCRIPTION     : Camera follows projectile
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/26	Jayden Younger    		Camera follows projectile
  2024/10/7     Jayden Younger          Switching view
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour{
    static private FollowCamera S; // Another private Singleton
    static public GameObject POI; // point of interest

    public enum eView {none, Slingshot, castle, both}; // camera view types

    [Header("Inscribed")]
        public float easeing = 1.0f;
        public Vector2 minXY = Vector2.zero; // at 0
        public GameObject viewBothGO;

    [Header("Dynamic")]
        public float camZ; // desired Z pos
        public eView NextView = eView.Slingshot;


    // Awake is called once at instantiation
    void Awake(){
        S = this;

        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 destination = Vector3.zero;

        if(POI != null){
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if((poiRigid != null) && (poiRigid.IsSleeping())) {
                POI = null;
            }
        }
        if(POI != null){
            destination = POI.transform.position;
        }
        //destination.x = Mathf.Max(minXY.x, destination.x);
        //destination.x = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easeing);

        destination.z = camZ;

        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
    public void SwitchView(eView NewView){
        if(NewView == eView.none){
            NewView = NextView;
        }
        switch(NewView){
            case eView.Slingshot:
                POI = null;
                NewView = eView.castle;
                break;
            case eView.castle:
                POI = MissionDemolition.Get_Castle();

                NextView = eView.both;
                break;
            case eView.both:
                POI = viewBothGO;
                NextView = eView.Slingshot;
                break;
        }
    }
    public void SwitchView(){
        SwitchView(eView.none);
    }
    static public void SWITCH_VIEW(eView newView){
        S.SwitchView(newView);
    }
}
