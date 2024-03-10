using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private Transform rotateToMouse;
    Camera playerCam;
    Vector2 mousePos;
    Vector2 dir;
=======
    Camera playerCam;
    Vector2 mousePos;
>>>>>>> b97a01a044dbcd1d1bc925652876e6e95023376f
    private void Start() {
        playerCam = GetComponent<Camera>();
    } 
    private void Update() {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
<<<<<<< HEAD
        dir = mousePos - new Vector2(transform.position.x, transform.position.y);
        rotateToMouse.up = dir;
=======
>>>>>>> b97a01a044dbcd1d1bc925652876e6e95023376f
    }
}   
