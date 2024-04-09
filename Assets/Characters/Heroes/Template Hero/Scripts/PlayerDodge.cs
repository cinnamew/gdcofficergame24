using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    PlayerMovement moveScript;
    float dodgeDuration; //does not necessarily have to be the length of the animation
    AnimController pAnimator;

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

    // string GetAnimState() {
    //     return pAnimator.GetCurrentAnimatorStateInfo(0).ToString();
    // }

    public string GetRandomAnimString(string animPrefix, int numOfAnims) {
        return animPrefix + Time.frameCount%numOfAnims; //starts from 0 btw
    }
    void GetRandomDodgeAnim() {
        pAnimator.SetAnimState(GetRandomAnimString(AnimController.DODGE_PREFIX, 1));
    }
}
