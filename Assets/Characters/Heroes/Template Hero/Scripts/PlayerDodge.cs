using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    Animator pAnimator;
    PlayerMovement moveScript;

    private void Start() {
        moveScript = GetComponent<PlayerMovement>();
    }
    public void Dodge() {
        //invincibility during dodge
        while (GetAnimState() == AnimController.DODGE) {
            //insert inv code here
            //disables movement
            moveScript.SetCanMove(false);
            pAnimator.Play(AnimController.DODGE);
            return;
        }
        moveScript.SetCanMove(true);
        //disable inv
    }

    string GetAnimState() {
        return pAnimator.GetCurrentAnimatorStateInfo(0).ToString();
    }
}
