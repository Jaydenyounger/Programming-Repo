/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Apple picker
* FILE NAME       : HighScore.cs
* DESCRIPTION     : Gets the high score
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/15	Jayden Younger    		Added the ability to get the highest score
* 
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour{
    static private int _SCORE = 1000; // the default high score
    static private TMP_Text uiText; 


    void Start(){
        uiText = GetComponent<TMP_Text>();
            if(PlayerPrefs.HasKey("HighScore")){
                SCORE = PlayerPrefs.GetInt("HighScore");
            }
        PlayerPrefs.SetInt("HighScore", SCORE);
    }


    static public int SCORE {
        get {return _SCORE; }
        private set {
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", value);
            if(uiText != null){
                uiText.text = "High Score: " + value.ToString("#,0");
            }
        }
    }


static public void TRY_SET_HIGH_SCORE (int scoreToTry){
    if(scoreToTry <= SCORE) return; //If scoreToTry is too low, return
        SCORE = scoreToTry;
}


[Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
public bool resetHighScoreNow = false;
void OnDrawGizmos(){
    if(resetHighScoreNow){
        resetHighScoreNow = false;
        PlayerPrefs.SetInt("HighScore", 1000);
        Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
    }
}
    
}
