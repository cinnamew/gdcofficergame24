using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    float moveSpeed;
<<<<<<< HEAD
    bool canMove = true;
=======
>>>>>>> b97a01a044dbcd1d1bc925652876e6e95023376f
    Vector2 moveDirection;
    [SerializeField]
    //float dodgeSpeed;
    Rigidbody2D rb;
    bool isOnSlipperySurface;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!isOnSlipperySurface) {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
        } else {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.y = Input.GetAxis("Vertical");
            //might be useful if we do a winter-themed stage (e.g. WMJ)
        }
<<<<<<< HEAD
        if(canMove)rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
=======
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
>>>>>>> b97a01a044dbcd1d1bc925652876e6e95023376f
    }
}
