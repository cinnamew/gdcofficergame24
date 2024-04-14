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
    bool hasDodgeAnims = true;
    PlayerMovement moveScript;
    SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        pAnimator = GetComponent<Animator>();
        aimpointRef = GameObject.FindGameObjectWithTag("AimPointer").GetComponent<AimpointSpriteManager>();
        if (hasDodgeAnims) {
            moveScript = GetComponent<PlayerMovement>();
        }
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDodgeAnims || (hasDodgeAnims && !moveScript.isDodging)) { //if not dodging
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
    }

    private void LateUpdate() {
        prevPos = newPos;
    }

    public void DodgeFlip() {
        Debug.Log("animstate is " + GetAnimState());
        if (!IsMovingForward()) {
            aimpointRef.GetCharSprite().flipX = !aimpointRef.GetCharSprite().flipX;
        }
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

    [ContextMenu("Is it dodging?")]
    bool PlayingDodgeAnim() {
        Debug.Log(pAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Dodge"));
        return pAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Dodge");
    }

    public string GetRandomAnimString(string animPrefix, int numOfAnims) {
        //Debug.Log("Your random frame is "+ Random.Range(0, numOfAnims));
        return animPrefix + Random.Range(0, numOfAnims); //starts from 0 btw
        //applied to dodges and parries (think of Pizza Tower taunts)
    }
}

