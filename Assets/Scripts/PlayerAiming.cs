using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAiming : MonoBehaviour
{

    Camera playerCam;
    Vector2 mousePos;
    public float mouseDistanceFromPlayer;
    Vector2 playerPos;
    Vector3 rotation;
    float rotZ;
    SpriteRenderer aimPointerSpriteRenderer;
    private PlayerAttack playerAttack;
    //ok
    private void Start() {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        aimPointerSpriteRenderer = GameObject.FindGameObjectWithTag("AimPointer").GetComponent<SpriteRenderer>();
        playerPos = GetPlayerV2();
        playerAttack = GetComponentInParent<PlayerAttack>();
    } 
    private void Update() {
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        mouseDistanceFromPlayer = Vector2.Distance(mousePos, GetPlayerV2());
        RotateAimpoint();
        ManageSpriteOrder();
        playerAttack.setAimDir(mousePos-playerPos);
    }
    Vector2 GetPlayerV2() {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }

    void RotateAimpoint() {
        rotation = (Vector3)mousePos - transform.position;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; //DUMB
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void ManageSpriteOrder() {
        if (rotZ > 0 && rotZ < 180) {
            //aimPointerSpriteRenderer.sortingOrder = Renderer.sortingOrder - 1;
        }
    }
    //ppp
}   