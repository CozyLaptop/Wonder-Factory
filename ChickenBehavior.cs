using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : MonoBehaviour{
    bool isMoving = false; //is chicken moving
    bool isWaiting = false; //is chicken waiting
    bool readyToWait = false; //ready to wait
    bool readyToMove = false; //ready to move
    float waitTime = 0f; //how many seconds to wait before moving
    float moveToPositionX; //pick a position close by
    float moveToPositionZ; //

    void Start(){
        if (Random.Range(1, 100) < 60){
            // will be true 60% of the time
            readyToWait = true; //set ready to start wait timer
        }
    }

    // Update is called once per frame
    void Update(){
        if (readyToWait){ //if ready to start timer
            readyToWait = false; 
            isWaiting = true;
            waitTime = Random.Range(2f, 10f);
        }
        if (waitTime > 0){
            waitTime -= Time.deltaTime;
            if (waitTime < 0){
                isWaiting = false;
            }
        }
        if (!isWaiting && !isMoving){
            readyToMove = true;
            var currentPosition = transform.position; //grabs current position
            moveToPositionX = (currentPosition.x + Random.Range(-3f, 3f)); //range to grab a new position
            moveToPositionZ = (currentPosition.z + Random.Range(-3f, 3f)); //
        }
        if (readyToMove){
            if (transform.position.x != moveToPositionX && transform.position.z != moveToPositionZ){ //if current position is not target position
                //move toward the target position
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveToPositionX, 0f, moveToPositionZ), Time.deltaTime * 1);
                isMoving = true;
            } else {
                readyToMove = false;
                isMoving = false;
                readyToWait = true;
            }
        }
    }
}