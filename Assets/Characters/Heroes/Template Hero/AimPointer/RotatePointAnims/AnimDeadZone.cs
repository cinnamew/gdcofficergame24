using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimDeadZone : MonoBehaviour
{
    //basically this script is for the gun-wobble animations for characters that use a weapon
    //separate from the player body sprite (e.g. mine)
    float deadZoneRadius;
    Animator RPointAnimator;
    const string IDLE_STATE = "Idle";
    const string MOVE_STATE = "Move Sway";
    const string BACKMOVE_STATE = "Backwards Move Sway";
    string pIdleAnimString;
    string pMoveAnimString;
    string pBackMoveAnimString;
    Animator playerAnimator;
    private void Start() {
        RPointAnimator = GetComponent<Animator>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        pIdleAnimString =  AnimController.IDLE;
        pMoveAnimString = AnimController.MOVE;
        pBackMoveAnimString = AnimController.BACKMOVE;
        deadZoneRadius = 2.5f;
    }

    private void Update() {
        if (GetMouseDistance() < deadZoneRadius) {
            RPointAnimator.enabled = false;
        } else {
            RPointAnimator.enabled = true;
            HandleIdleSway();
            HandleMoveSway();
        }
    }

    float GetMouseDistance() {
        return GetComponent<PlayerAiming>().mouseDistanceFromPlayer;
    }

    void SyncIdleAnimation() { //Idle default has 3 frames at 5 samples per second. Might break with differing rates.
        RPointAnimator.Play(IDLE_STATE, 0, GetTimeOfAnimation(playerAnimator));
    }
    void SyncMoveAnimation() { //Both the move and backwards move have 6 frames at 12 samples per second.
        RPointAnimator.Play(MOVE_STATE, 0, GetTimeOfAnimation(playerAnimator));
    }

    void SyncBackMoveAnimation() { //Both the move and backwards move have 6 frames at 12 samples per second.
        RPointAnimator.Play(BACKMOVE_STATE, 0, GetTimeOfAnimation(playerAnimator));
    }

    float GetTimeOfAnimation(Animator animator) {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    float TimeDifferenceBetween(Animator a, Animator b) {
        return Math.Abs(GetTimeOfAnimation(RPointAnimator) - GetTimeOfAnimation(playerAnimator));
    }
    void HandleIdleSway() {
        if (TimeDifferenceBetween(RPointAnimator, playerAnimator) > 0.1f) {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(pIdleAnimString)) {
                SyncIdleAnimation();
                print("syncing idle");
                return;
            }
        }
    }
    
    void HandleMoveSway() {
        if (TimeDifferenceBetween(RPointAnimator, playerAnimator) > 0.2f) {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(pMoveAnimString)) {
                SyncMoveAnimation();
                print("syncing move");
                return;
            }
        }
        if (TimeDifferenceBetween(RPointAnimator, playerAnimator) > 0.2f) {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(pBackMoveAnimString)) {
                SyncBackMoveAnimation();
                print("syncing backmove");
                return;
            }
        }
    }


}
