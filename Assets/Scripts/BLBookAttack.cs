using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLBookAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject rotateObject;
    [SerializeField] private float rotationSpeed = 400f; 
    [SerializeField] private float visibleTime = 7f;
    [SerializeField] private GameObject[] BLBookFormations;
    private int numBooks = 3;

    void FixedUpdate()
    {
        rotateObject = BLBookFormations[numBooks - 3];
        Transform objTransform = rotateObject.GetComponent<Transform>();
        objTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        Transform[] bookTransforms = BLBookFormations[numBooks - 3].GetComponentsInChildren<Transform>();
        for (int i = 0; i < bookTransforms.Length; i++)
        {
            bookTransforms[i].Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
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
            numBooks += 1;
        }
    }
}
