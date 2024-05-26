using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats stats;
    [SerializeField] float defaultMoveSpeed = 2;
    float moveSpeed;
    public Vector2 moveDirection;
    Rigidbody2D rb;
    PlayerDodge dodgeScript;
    bool isOnSlipperySurface = false;
    float dodgeDuration;
    public BoolTimer canMove;
    public BoolTimer isInvincible;
    public BoolTimer isDodging;
    private StatsManager statsManager;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        stats = (PlayerStats)ScriptableObject.CreateInstance(typeof(PlayerStats));
        dodgeScript = GetComponent<PlayerDodge>();
        moveSpeed = defaultMoveSpeed;
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    private void Update() {
        if (statsManager != null){
            moveSpeed = defaultMoveSpeed*(statsManager.Spd/100f);
        }
        if (!isOnSlipperySurface) {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
        } else {
            //moveDirection.x = Input.GetAxis("Horizontal");
            //moveDirection.y = Input.GetAxis("Vertical");
        }
        moveDirection.Normalize();
        if (IsIntentionallyMoving() && Input.GetKeyDown(KeyCode.F) && !isDodging) { //was originally C 
            Dodge();
        } //if not moving, then parry/taunt
        
        
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

    public IEnumerator IncreaseSpeedForSeconds(float seconds) {
        SetMoveSpeed(dodgeScript.GetDodgeSpeed());
        yield return new WaitForSeconds(seconds);
        SetMoveSpeed(defaultMoveSpeed);
    }

    void SetMoveSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }
    private void FixedUpdate() {
        //if (canMove) {
            //rb.velocity = (moveDirection + moveSpeed * Time.fixedDeltaTime * rb.position);
            if (!isDodging) {
                rb.velocity = moveDirection * moveSpeed;
            } else {
                rb.velocity = dodgeScript.GetDodgeDirection() * dodgeScript.GetDodgeSpeed();
            }
            
        //}

    }

    bool IsIntentionallyMoving() { //in case the player will get pushed around.
        return transform.hasChanged && (Input.GetAxisRaw("Horizontal") != 0 ||
        Input.GetAxisRaw("Vertical") != 0);
    }
}