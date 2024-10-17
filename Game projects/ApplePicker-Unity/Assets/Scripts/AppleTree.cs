/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : Apple Picker
* FILE NAME       : AppleTree
* DESCRIPTION     : Moves the tree left or right at a certain speed every frame
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2000/01/01		Developer's Name    		 Created <short comment of changes>
* 
* 2024/08/09        Jayden Younger               Added Basic Movement
* 2024/10/09        Jayden Younger               Dropping Apples
*
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour{
    [Header("inscribed")]

    public GameObject applePrefab;// calls prefab for instantiating apples
    public GameObject goldenApplePrefab;// calls prefab for instantiating golden apples
    public float speed = 1f;// Speed of the apple tree
    public float leftAndRightEdge = 10f;// Distance where AppleTree moves turns around
    public float changeDirChance = 0.1f;// chance that the apple will change directions
    public float appleDropDelay = 1f;// delay between apples instantiations

    // Start is called before the first frame update
    void Start(){
    // Start dropping apples
        Invoke("DropApple", 2f);
    }

    void DropApple(){
        
        GameObject apple;

            int AppleNumberGeneration = Random.Range(0,6);
            if(AppleNumberGeneration == 0){
                apple = Instantiate<GameObject>(goldenApplePrefab);
                    apple.transform.position = transform.position;

                    speed += .05f;

                    Invoke("DropApple", appleDropDelay);
            }else{ 
                apple = Instantiate<GameObject>(applePrefab);
                    apple.transform.position = transform.position;

                    speed += .02f;
                    
                    Invoke("DropApple", appleDropDelay);
            }
            
    }

    // Update is called once per frame
    void Update(){
    // basic movement
        Vector3 pos = transform.position; 
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

    // Changing Direction
        if (pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed);// move right
        } 
            else if (pos.x > leftAndRightEdge){
                speed = -Mathf.Abs(speed);// move left
            } 
                /*
                else if(Random.value < changeDirChance){
                    speed *= -1; // change direction
                }
                */
    }

    void FixedUpdate() {
        // Random direction changes are now time base due to FixedUpdate()
        if(Random.value < changeDirChance){
            speed *= -1; // change direction
        }
    }

    
}
