/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : MenuFunctions.cs
* DESCRIPTION     : Short Description of script.
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/15	Jayden Younger    		Added play, reset, and quit game functions
* 
*
/******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SplashScreens : MonoBehaviour{
  
  public void PlayGame(){
    SceneManager.LoadSceneAsync(1);
  }
  public void QuitGame(){
    Application.Quit();
  }

  public void ResetGame(){
    SceneManager.LoadScene(1);
  }

  public void OpenMenu(){
    SceneManager.LoadSceneAsync(0);
  }


}