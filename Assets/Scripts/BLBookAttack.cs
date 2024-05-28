using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLBookAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject rotateObject;
    [SerializeField] private float rotationSpeed = 100f; 
    [SerializeField] private float visibleTime = 4f;
    [SerializeField] private float hiddenTime = 6f;
    [SerializeField] private GameObject[] BLBookFormations;
    private int numBooks = 3;
    private float timeOfLastAttack;
    private bool showing = true;
    void FixedUpdate()
    {
        if (Time.time - timeOfLastAttack >= visibleTime && showing){ //this logic might be flawed
            showing = false;
            BLBookFormations[numBooks-4].SetActive(showing);
            timeOfLastAttack = Time.time;
        }
        if (Time.time - timeOfLastAttack >= hiddenTime && !showing){
            showing = true;
            BLBookFormations[numBooks-4].SetActive(showing);
            timeOfLastAttack = Time.time;
        }
        rotateObject = BLBookFormations[numBooks - 4];
        Transform objTransform = rotateObject.GetComponent<Transform>();
        objTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        Transform[] bookTransforms = BLBookFormations[numBooks - 4].GetComponentsInChildren<Transform>();
        Debug.Log("LENGHTIPEJHTOIETHOIEHTOIPHTOPIEHITPOHPIOTEWHOITWHIOPTHEOIPWTHIPO" + bookTransforms.Length);
        for (int i = 0; i < bookTransforms.Length; i++)
        {
            if (bookTransforms[i] != objTransform){
                bookTransforms[i].gameObject.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void upgradeBooks(){
        Debug.Log("APOIJFPJSDFJOSJFOJSOPDFJSJFJSPOFJSPOFJIP ORBBB");
        if (numBooks-3 < BLBookFormations.Length)
        {
            BLBookFormations[numBooks-3].SetActive(true);
            if (numBooks > 3){
                BLBookFormations[numBooks-4].SetActive(false);
            }
            showing = true;
            timeOfLastAttack = Time.time;
            numBooks += 1;
        }
    }
}
