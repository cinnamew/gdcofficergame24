using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBallAttack : MonoBehaviour
{
    [SerializeField] private GameObject rotateObject;
    [SerializeField] private float rotationSpeed = 400f; 
    [SerializeField] private GameObject[] SteelBalls;
    private int numOrbs = 1;
    private float degreesRotated = 0;
    private float degreesTillHidden = 360f;
    private float hiddenTime = 3f;
    private float lastHiddenTime;
    private bool hidden = false;
    void FixedUpdate()
    {
        if (!hidden){
            Transform objTransform = rotateObject.GetComponent<Transform>();
            objTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            degreesRotated += rotationSpeed * Time.deltaTime;
            if (degreesRotated >= degreesTillHidden){
                hidden = true;
                rotateObject.SetActive(false);
                lastHiddenTime = Time.time;
            }
            for (int i = 0; i < SteelBalls.Length; i++)
            {
                SteelBalls[i].transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        } else if (Time.time - lastHiddenTime >= hiddenTime){
            hidden = false;
            rotateObject.SetActive(true);
            degreesRotated = 0f;
        }
    }

    public void SetSteelBallActive(){
        rotateObject.SetActive(true);
        for (int i = 0; i < SteelBalls.Length; i++)
        {
            SteelBalls[i].SetActive(true);
        }

    }
    public void SetDamage(int newVal){
        for (int i = 0; i < SteelBalls.Length; i++){
            SteelBalls[i].GetComponent<HeroHitbox>().setDamage(newVal);
        }
    }

    public void SetDegreesTillHidden(float newVal){
        degreesTillHidden = newVal;
    }
}
