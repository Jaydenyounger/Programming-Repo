/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Applepicker
* FILE NAME       : ApplePicker.cs
* DESCRIPTION     : Adds baskets into the scene
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/10	Jayden Younger    		Add baskets to the bottom of the scene
* 2024/09/15    Jayden Younger          When the player misses 3 apples, show the game over screen
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour{
    [Header("Incribed")]
    public GameObject basketPrefab; // our basket object in scene
    public int numBasket = 3; // the number of baskets
    public float basketBottomY = -14f; // Starting location
    public float basketSpacingY = 2f; // In between basket spacing
    public List<GameObject> basketList;
    public GameObject GameOverScreen; // get the game over screen
    public GameObject ScoreCounter; // get the score counter
    public GameObject HighScore; // get the high counter


    
    void Awake(){ // Awake is called once at instantiation
        GameOverScreen.SetActive(false); // set the game over screen to false
        ScoreCounter.SetActive(true);
        HighScore.SetActive(true);
    }

    
    void Start(){ // Start is called before the first frame update
        basketList = new List<GameObject>();
        // adds baskets into the scene
        for (int i = 0; i<numBasket; i++){ // Loop 
            GameObject tBasketGo = Instantiate<GameObject>(basketPrefab);// Add basket
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i); // Space basket and transform
            tBasketGo.transform.position = pos; 
            basketList.Add(tBasketGo);
        } 
    }

    public void AppleMissed(){
    // Destroy One Basket
    // Get the index of the last Basket in basketList
        int basketIndex = basketList.Count -1;
    // get a referance to that Basket GameObject
        GameObject basketGo =basketList[basketIndex];
    //Remove the Basket from the list and destroy the gameobject
        basketList.RemoveAt(basketIndex);
        Destroy(basketGo);

    // If there are no Baskets left, activate game over screen
        if (basketList.Count == 0) {
            //SceneManager.LoadScene("Level_01");
            GameOverScreen.SetActive(true); // Show the game over
            ScoreCounter.SetActive(true);
            HighScore.SetActive(false);
        }
    }
}
