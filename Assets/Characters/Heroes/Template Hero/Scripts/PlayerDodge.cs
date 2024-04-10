using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    PlayerMovement moveScript;
    float dodgeDuration; //does not necessarily have to be the length of the animation
    AnimController pAnimator;
    int numOfDodgeAnims;

    private void Start() {
        moveScript = GetComponent<PlayerMovement>();
    }
    public void Dodge() {
        pAnimator.SetAnimState(AnimController.DODGE_PREFIX);
        //at this point, the dodge animation is already playing
        //invincibility during dodge
        moveScript.canMove.Set(dodgeDuration, false);
        moveScript.isInvincible.Set(dodgeDuration);
        
        //disable inv
    }
    
    void GetRandomDodgeAnim() {
        pAnimator.SetAnimState(pAnimator.GetRandomAnimString(AnimController.DODGE_PREFIX, numOfDodgeAnims));
        //DODGE_0 should always have no animation. Just the visual dodge effect pasted over
        //a still frame of the move animation. So it doesn't really count as one animation
    }
}
