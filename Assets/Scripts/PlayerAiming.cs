using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{

    [SerializeField] private Transform rotateToMouse;
    Camera playerCam;
    Vector2 mousePos;
    Vector2 dir;

    private void Start() {
        playerCam = GetComponent<Camera>();
    } 
    private void Update() {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

        dir = mousePos - new Vector2(transform.position.x, transform.position.y);
        rotateToMouse.up = dir;
    }
}   
