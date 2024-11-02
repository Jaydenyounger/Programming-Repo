/*******************************************************************
* COPYRIGHT       : Year
* PROJECT         : Name of Project or Assignment script is used for.
* FILE NAME       : UIActions.cs
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
using UnityEngine.SceneManagement;

public class UIActions : MonoBehaviour{
      public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
      }
      public void LoadMainMenu(){
        SceneManager.LoadSceneAsync(0);
      }
      public void LoadEndScreen(){
        SceneManager.LoadSceneAsync(2);
      }
      public void Quit(){
        Application.Quit();
      }
}
