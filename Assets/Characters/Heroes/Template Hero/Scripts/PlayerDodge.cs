using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDodge : MonoBehaviour
{
    PlayerMovement moveScript;
    float dodgeDuration; //does not necessarily have to be the length of the animation
    float dodgeSpeed = 30;
    AnimController pAnimator;
    int numOfDodgeAnims = 1;

    private void Start() {
        moveScript = GetComponent<PlayerMovement>();
        pAnimator = GetComponent<AnimController>();
    }
    public void Dodge() {
        Debug.Log("the string is " + AnimController.DODGE_PREFIX + Random.Range(0,numOfDodgeAnims));
        GetRandomDodgeAnim();
        //at this point, the dodge animation is already playing
        //invincibility during dodge
        //push the player forward toward direction
        //moveScript.canMove.Set(dodgeDuration, false);
        moveScript.isInvincible.Set(dodgeDuration);
        moveScript.GetRb().velocity = (moveScript.moveDirection + dodgeSpeed * Time.fixedDeltaTime * moveScript.GetRb().position);
        //disable inv
    }
    
    void GetRandomDodgeAnim() {
        pAnimator.GetRandomAnimString("Dodge_", 1);
        
        //DODGE_0 should always have no animation. Just the visual dodge effect pasted over
        //a still frame of the move animation. So it doesn't really count as one animation
    }
}