using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats stats;
    [SerializeField] float moveSpeed;
    // bool canMove = true;
    //bool canDodge;
    Vector2 moveDirection;
    [SerializeField]
    //float dodgeSpeed;
    Rigidbody2D rb;
    bool isOnSlipperySurface;
    float dodgeDuration;
    public BoolTimer canMove;
    public BoolTimer isInvincible;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        stats = (PlayerStats)ScriptableObject.CreateInstance(typeof(PlayerStats));
    }

    private void Update() {
        if (!isOnSlipperySurface) {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
        } else {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.y = Input.GetAxis("Vertical");
        }

        if (Input.GetKey(KeyCode.D) && CanDodge()) {
            Dodge();
        }
        //stats.Spd = moveSpeed;
        //Debug.Log(stats.Spd);
    }

    void Dodge() {
        //stop the player from moving around during dodge
        //render the player invulnerable during dodge
        //play dodge animation
        //boost player speed during dodge
        //reverse steps 1 and 2
        canMove.Set(0.1f, false);
        isInvincible.Set(0.1f);


    }

    IEnumerator PausePlayerMovement(float dodgeTime) {
        yield return new WaitForSeconds(dodgeTime);
        canMove.Set(0.1f, false);
        
    }

    // public void SetCanMove(bool canMove)
    // {
    //     this.canMove = canMove;
    // }

    private void FixedUpdate() {
        if (canMove) {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

    }
    bool CanDodge() {
        return IsIntentionallyMoving();
    }

    bool IsIntentionallyMoving() { //in case the player will get pushed around.
        return transform.hasChanged && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
    }
}