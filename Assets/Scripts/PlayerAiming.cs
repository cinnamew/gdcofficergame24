using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    Camera playerCam;
    Vector2 mousePos;
    private void Start() {
        playerCam = GetComponent<Camera>();
    } 
    private void Update() {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
    }
}   
