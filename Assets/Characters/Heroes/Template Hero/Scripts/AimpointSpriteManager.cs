using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimpointSpriteManager : MonoBehaviour
{
    //this script also handles the flipping of the player character
    SpriteRenderer characterSprite;
    Transform rotationPoint;
    SpriteRenderer thisSprite;
    int flipAngleBuffer;
    float orderFlipRightThreshold;
    float orderFlipLeftThreshold;
    float bufferDistance;
    void Start()
    {
        rotationPoint = GameObject.FindGameObjectWithTag("PlayerAimRotationPoint").GetComponent<Transform>();
        thisSprite = GetComponent<SpriteRenderer>();
        characterSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        orderFlipRightThreshold = 0;
        orderFlipLeftThreshold = 180;
        bufferDistance = 0.25f; //keep at around 0.25f
    }
    void Update()
    {
        SwitchSortingOrder();
        FlipUpdate();
    }

    bool AimpointIsBehindPlayer() {
        return rotationPoint.rotation.z > orderFlipRightThreshold && rotationPoint.rotation.z < orderFlipLeftThreshold;
    }

    void SwitchSortingOrder() {
        if (AimpointIsBehindPlayer() && thisSprite.sortingLayerName != "BehindPlayer") {
            thisSprite.sortingLayerName = "BehindPlayer";
        } else if (thisSprite.sortingLayerName != "InFrontPlayer" && !AimpointIsBehindPlayer()) {
            thisSprite.sortingLayerName = "InFrontPlayer";
        }
    }
    public bool AimPointIsOnRightSide() { 
        //return rotationPoint.rotation.z > -90 && rotationPoint.rotation.z < 90; //always returns true for some reason...
        return thisSprite.transform.position.x > rotationPoint.position.x + bufferDistance;
    }
    void FlipUpdate() {
        if (AimPointIsOnRightSide() && !IsWithinBuffer() && thisSprite.flipY) { //if on right and is flipped
            thisSprite.flipY = false; //then unflip
            characterSprite.flipX = false;
        } else if (!AimPointIsOnRightSide() && !IsWithinBuffer() && !thisSprite.flipY) { //if on left and not already flipped
            thisSprite.flipY = true;
            characterSprite.flipX = true;
        }
    }
    bool IsWithinBuffer() {
        return Math.Abs(thisSprite.transform.position.x - rotationPoint.position.x) <= bufferDistance;
    }
}
