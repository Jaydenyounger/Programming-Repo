/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Misson Demolition
* FILE NAME       : MissionDemolition.cs
* DESCRIPTION     : Spawning castles
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/10/7 	Jayden Younger    		 Add the code to switch levels
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode{
        idle,
        playing,
        levelEnd
    }
public class MissionDemolition : MonoBehaviour{

    static private MissionDemolition S; // a private Singleton

    [Header("Inscribed")]
    public Text uitLevel;
    public Text uitShots;
    public Vector3 CastlePos;
    public GameObject[] Castles;
    public int MaxShots;

    [Header("Dynamic")]
    public int Level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string Showing = "Show Slingshot";

    public int CurrentShots;

    void Start(){
        S = this;
        levelMax = 0;
        levelMax = Castles.Length;
        StartLevel();
    }
    void StartLevel(){
        if(castle != null){
            Destroy(castle);
        }

        Projectile.DESTROY_PROJECTILES();

        castle = Instantiate<GameObject>(Castles[Level]);
        castle.transform.position = CastlePos;

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        FollowCamera.SWITCH_VIEW(FollowCamera.eView.both);
    }

    void UpdateGUI(){
        uitLevel.text = "Level: " + (Level + 1) + " of "+ levelMax;
        uitShots.text =  MaxShots - shotsTaken + " Shots left ";
    }

    // Update is called once per frame
    void Update(){
        UpdateGUI();
        if((mode == GameMode.playing) && Goal.goalMet){
            mode = GameMode.levelEnd;
            FollowCamera.SWITCH_VIEW(FollowCamera.eView.both);
            Invoke("NextLevel", 2f);
        }
        CurrentShots = MaxShots - shotsTaken;
         if (CurrentShots < 0 ){
            Level = 0;
            shotsTaken = 0;
            StartLevel();
        }
    }
    void NextLevel(){
        Level++;
        if(Level == levelMax){
            shotsTaken = 0;
            Level = 0;
        }
        StartLevel();
    }

    static public void SHOT_FIRED(){
        S.shotsTaken++;
    }

    static public GameObject Get_Castle() {
        return S.castle;
    }
    // Awake is called once at instantiation
    void Awake()
    {
        
    }
}
