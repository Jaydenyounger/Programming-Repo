/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Applepicker
* FILE NAME       : Basket.cs
* DESCRIPTION     : Player Movement and Basket collision
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/09/12	Jayden Younger    		Added the boasket movment and Apple collison
* 2024/09/15    Jayden Younger          Detects the golden apple and give extra points to the player
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour{
    public ScoreCounter ScoreCounter;

    public GameObject appleTree;


    void Start(){ // Start is called before the first frame update
        GameObject scoreGo = GameObject.Find("ScoreCounter");

        ScoreCounter = scoreGo.GetComponent<ScoreCounter>();
    }

    
    void Update(){ // Update is called once per frame
        // updates the basket posistion when the mouse moves
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }


    private void OnCollisionEnter(Collision collision){
        GameObject CollidedWith = collision.gameObject; //get the object that is collided with the basket
            if (CollidedWith.CompareTag("Apple")) { // check if the object is a apple
                Destroy(CollidedWith); //Destroy the apple
                    ScoreCounter.Score += 100;//Increase Score
                    HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.Score); // try to set the high score
            } else if (CollidedWith.CompareTag("Golden Apple")){ // check if the object is a golden apple
                Destroy(CollidedWith); //Destroy the golden apple
                    ScoreCounter.Score += 200;//Increase Score
                    HighScore.TRY_SET_HIGH_SCORE(ScoreCounter.Score); // try to set the high score
        }
    }
}
