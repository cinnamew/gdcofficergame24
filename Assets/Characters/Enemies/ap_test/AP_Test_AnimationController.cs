using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_Test_AnimationController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    string curState;
    public const string FORWARD = "ap_test_bounce_forward";
    public const string BACKWARD = "ap_test_bounce_backward";
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        curState = FORWARD;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > 0)
        {
            ChangeAnimationState(FORWARD);
        }else if(rb.velocity.x < 0)
        {
            ChangeAnimationState(BACKWARD);
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (newState == curState) return;
        if(newState == FORWARD)
        {
            Debug.Log("forward");
            anim.Play(FORWARD);
        }else if(newState == BACKWARD)
        {
            Debug.Log("backward");
            anim.Play(BACKWARD);
        }
        curState = newState;
    }
}
