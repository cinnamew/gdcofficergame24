using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator pAnimator;
    Rigidbody2D playerRb;
    string currentAnimState;

    //anim names
    public const string MOVE = "Move";
    public const string IDLE = "Idle";
    public const string BACKMOVE = "BackwardsMove";

    public const string DODGE_PREFIX = "Dodge_";

    Transform prevPos;
    Transform newPos;
    AimpointSpriteManager aimpointRef;

    void Start()
    {
        pAnimator = GetComponent<Animator>();
        aimpointRef = GameObject.FindGameObjectWithTag("AimPointer").GetComponent<AimpointSpriteManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsIntentionallyMoving()) {
            if (IsMovingForward())  {
                SetAnimState(MOVE);
            } else { //if doing the michael jackson
                SetAnimState(BACKMOVE);
            }
        } else {
            SetAnimState(IDLE);
        }
    }

    private void LateUpdate() {
        prevPos = newPos;
    }

    bool IsIntentionallyMoving() { //in case the player will get pushed around.
        return transform.hasChanged && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
    }

    bool IsMovingForward() {
        return aimpointRef.AimPointIsOnRightSide() && Input.GetAxisRaw("Horizontal") > 0 ||
        !aimpointRef.AimPointIsOnRightSide() && Input.GetAxisRaw("Horizontal") < 0 ||
        IsOnlyMovingVertically();
    }

    bool IsOnlyMovingVertically() {
        return Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") != 0;
    }

    public void SetAnimState(string newState) {
        //stops animation from interrupting itself
        if (currentAnimState == newState)
        {
            return;
        }
        //plays the new anim
        pAnimator.Play(newState);
        //update the currAnimState variable
        currentAnimState = newState;    

    }
    public string GetAnimState() {
        return currentAnimState;
    }
}

