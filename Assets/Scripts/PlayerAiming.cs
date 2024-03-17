using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{

    Camera playerCam;
    Vector2 mousePos;
    Vector3 rotation;
    float rotZ;
    SpriteRenderer aimPointerSpriteRenderer;
    //ok
    private void Start() {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        aimPointerSpriteRenderer = GameObject.FindGameObjectWithTag("AimPointer").GetComponent<SpriteRenderer>();
    } 
    private void Update() {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

        RotateAimpoint();
        ManageSpriteOrder();
    }

    void RotateAimpoint() {
        rotation = (Vector3)(mousePos) - (Vector3)(transform.position);
        rotZ = (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg); //DUMB
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void ManageSpriteOrder() {
        if (rotZ > 0 && rotZ < 180) {
            //aimPointerSpriteRenderer.sortingOrder = this,sortingOrder - 1;
        }
    }
    //ppp
}   