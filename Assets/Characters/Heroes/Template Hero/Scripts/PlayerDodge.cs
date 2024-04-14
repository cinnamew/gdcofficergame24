using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDodge : MonoBehaviour
{
    PlayerMovement moveScript;
    float dodgeDuration; //does not necessarily have to be the length of the animation
    float dodgeSpeed = 3.5f;
    AnimController pAnimator;
    int numOfDodgeAnims = 1;
    //I need to get the last vector2 of the movement right before the dash
    Vector2 dodgeVector2;
    AimpointSpriteManager aimSpriteScript;

    private void Start() {
        moveScript = GetComponent<PlayerMovement>();
        pAnimator = GetComponent<AnimController>();
        aimSpriteScript = GetComponentInChildren<AimpointSpriteManager>();
        dodgeDuration = 1f;
    }
    public void Dodge() {
        Debug.Log("the string is " + AnimController.DODGE_PREFIX + Random.Range(0,numOfDodgeAnims));

        dodgeVector2 = moveScript.moveDirection;
        pAnimator.DodgeFlip();
        GetRandomDodgeAnim();
        //StartCoroutine(moveScript.SetDodgeSpeedMultiplier(dodgeSpeedMult, dodgeDuration));
        //at this point, the dodge animation is already playing
        //invincibility during dodge
        //push the player forward toward direction
        aimSpriteScript.pointerIsHidden.Set(dodgeDuration);
        moveScript.isInvincible.Set(dodgeDuration);
        moveScript.isDodging.Set(dodgeDuration);
        
        aimSpriteScript.isDodging.Set(dodgeDuration); //disables player sprite flipping during dodge
    }


    public float GetDodgeSpeed() {
        return dodgeSpeed;
    }
    
    public Vector2 GetDodgeDirection() {
        return dodgeVector2;
    }

    void GetRandomDodgeAnim() {
        pAnimator.SetAnimState(pAnimator.GetRandomAnimString("Dodge_", 1));
        Debug.Log("PLAYING DODGE ANIMATION");
        //DODGE_0 should always have no animation. Just the visual dodge effect pasted over
        //a still frame of the move animation. So it doesn't really count as one animation
    }
}