using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDodge : MonoBehaviour
{
    PlayerMovement moveScript;
    float dodgeDuration; //does not necessarily have to be the length of the animation
    public float dodgeSpeed = 3.5f;
    AnimController animScript;
    [SerializeField] private int numOfDodgeAnims;
    //I need to get the last vector2 of the movement right before the dash
    Vector2 dodgeVector2;
    AimpointSpriteManager aimSpriteScript;

    private void Start() {
        moveScript = GetComponent<PlayerMovement>();
        animScript = GetComponent<AnimController>();
        aimSpriteScript = GetComponentInChildren<AimpointSpriteManager>();
        dodgeDuration = 1f;
    }
    public void Dodge() {
        Debug.Log("the string is " + AnimController.DODGE_PREFIX + Random.Range(0,numOfDodgeAnims));

        dodgeVector2 = moveScript.moveDirection;
        animScript.DodgeFlip();
        GetRandomDodgeAnim();
        //animScript.SetAnimState(AnimController.DODGE_PREFIX + "2"); //DEBUGGING!

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
        animScript.SetAnimState(animScript.GetRandomAnimString(AnimController.DODGE_PREFIX, numOfDodgeAnims));
        //Debug.Log("PLAYING DODGE ANIMATION");
        //DODGE_0 should always have no animation. Just the visual dodge effect pasted over
        //a still frame of the move animation. So it doesn't really count as one animation
    }
}