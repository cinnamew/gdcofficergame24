using System;
using System.Collections;
using UnityEngine;

public class AimpointSpriteManager : MonoBehaviour
{
    //this script also handles the flipping of the player character
    SpriteRenderer characterSprite;
    Transform rotationPoint;
    SpriteRenderer thisSprite;
    public float orderFlipRightThreshold;
    float orderFlipLeftThreshold;
    float flipBufferDistance;
    float orderBufferDistance;
    public BoolTimer pointerIsHidden;
    public BoolTimer isDodging;
    float flipBufferAngle;
    public float rotationAngle;
    void Start()
    {
        rotationPoint = GameObject.FindGameObjectWithTag("PlayerAimRotationPoint").GetComponent<Transform>();
        thisSprite = GetComponent<SpriteRenderer>();
        characterSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        orderFlipRightThreshold = 30;
        orderFlipLeftThreshold = 165;
        flipBufferAngle = 20;
        flipBufferDistance = 0.001f; 
        orderBufferDistance = 0.00f;
        
    }
    void Update()
    {
        SwitchSortingOrder();
        FlipUpdate();
        if (!thisSprite.enabled && !pointerIsHidden) {
            ShowPointerSprite();
            
        } else if (thisSprite.enabled && pointerIsHidden) {
            HidePointerSprite();
        }
        if (!isDodging) {
            FlipAfterDodge();
        }
        rotationAngle = rotationPoint.eulerAngles.z;
    }

    bool AimpointIsBehindPlayer() {
        return rotationPoint.eulerAngles.z > orderFlipRightThreshold && rotationPoint.eulerAngles.z < orderFlipLeftThreshold;
    }

    void SwitchSortingOrder() {
        if (!IsWithinOrderBuffer()) {
            if (AimpointIsBehindPlayer() && thisSprite.sortingLayerName != "BehindPlayer") {
                thisSprite.sortingLayerName = "BehindPlayer";
            } else if (thisSprite.sortingLayerName != "InFrontPlayer" && !AimpointIsBehindPlayer()) {
                thisSprite.sortingLayerName = "InFrontPlayer";
            }
        }
    }

    public void HidePointerSprite() {
        thisSprite.enabled = false;

    }

    public IEnumerator HidePointerSpriteForSeconds(float seconds) {
        HidePointerSprite();
        yield return new WaitForSeconds(seconds);
        ShowPointerSprite();
    }
    public void ShowPointerSprite() {
        thisSprite.enabled = true;
    }
    
    public bool AimPointIsOnRightSide() { 
        //return rotationPoint.rotation.z > -90 && rotationPoint.rotation.z < 90; //always returns true for some reason...
        return thisSprite.transform.position.x > rotationPoint.position.x + flipBufferDistance;
    }
    public void FlipUpdate() {
        if (!isDodging) {
            //to turn right
            if (AimPointIsOnRightSide() && !IsWithinFlipBuffer() && thisSprite.flipY) { //if on right and is flipped
                thisSprite.flipY = false; //then unflip
                characterSprite.flipX = false;
            } else if (!AimPointIsOnRightSide() && !IsWithinFlipBuffer() && !thisSprite.flipY) { //if on left and not already flipped
                thisSprite.flipY = true;
                characterSprite.flipX = true;
            }
        } else {
        }
    }

    public void FlipAfterDodge() {
        if (!IsWithinFlipBuffer()) {
            if ((AimPointIsOnRightSide() && characterSprite.flipX) || (!AimPointIsOnRightSide() && !characterSprite.flipX)) {
                characterSprite.flipX = !characterSprite.flipX;
            }
        }
    }


    bool IsWithinFlipBuffer() {
        return Math.Abs(thisSprite.transform.position.x - rotationPoint.position.x) <= flipBufferDistance &&
        (DistanceFrom90Degrees() > flipBufferAngle || DistanceFrom270Degrees() > flipBufferAngle);
    }
    bool IsWithinOrderBuffer() {
        return Math.Abs(thisSprite.transform.position.y - rotationPoint.position.y) <= orderBufferDistance;
    }

    float DistanceFrom90Degrees() {
        return Math.Abs(90 - rotationPoint.eulerAngles.z);
    }
    float DistanceFrom270Degrees() {
        return Math.Abs(270 - rotationPoint.eulerAngles.z);
    }

    public SpriteRenderer GetCharSprite() {
        return characterSprite;
    }
}
