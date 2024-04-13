using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats stats;
    [SerializeField] float moveSpeed;
    // bool canMove = true;
    //bool canDodge;
    public Vector2 moveDirection;
    [SerializeField]
    //float dodgeSpeed;
    Rigidbody2D rb;
    PlayerDodge dodgeScript;
    bool isOnSlipperySurface;
    float dodgeDuration;
    public BoolTimer canMove;
    public BoolTimer isInvincible;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        stats = (PlayerStats)ScriptableObject.CreateInstance(typeof(PlayerStats));
        dodgeScript = GetComponent<PlayerDodge>();
    }

    private void Update() {
        if (!isOnSlipperySurface) {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
        } else {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.y = Input.GetAxis("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.C) && CanDodge()) {
            Dodge();
        } //if not moving, then parry/taunt
        //stats.Spd = moveSpeed;
        //Debug.Log(stats.Spd);
    }
    public Rigidbody2D GetRb() {
        return rb;
    }
    void Dodge() {
        //stop the player from moving around during dodge
        //render the player invulnerable during dodge
        //play dodge animation
        //boost player speed during dodge
        //reverse steps 1 and 2
        dodgeScript.Dodge();


    }

    // IEnumerator PausePlayerMovement(float dodgeTime) {
    //     yield return new WaitForSeconds(dodgeTime);
    //     canMove.Set(0.1f, false);
        
    // }

    private void FixedUpdate() {
        //if (canMove) {
            rb.velocity = (moveDirection + moveSpeed * Time.fixedDeltaTime * rb.position);
        //}

    }
    bool CanDodge() {
        return IsIntentionallyMoving();
    }

    bool IsIntentionallyMoving() { //in case the player will get pushed around.
        return transform.hasChanged && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
    }
}