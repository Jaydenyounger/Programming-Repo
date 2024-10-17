/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : ScoreCounter.cs
* DESCRIPTION     : Short Description of script.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/13	Jayden Younger    		Count the player score
* 2024/09/15    Jayden Younger          Edit text to add Score: The player score
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour{
    [Header("Dynamic")]
    public int Score = 0;

    private TMP_Text uiText;

    // Start is called before the first frame update
    void Start(){
        uiText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update(){
        uiText.text = "Score: " +Score.ToString("#,0"); // This 0 is Zero
    }
}
