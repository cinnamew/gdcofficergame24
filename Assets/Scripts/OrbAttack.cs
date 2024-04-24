using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject rotateObject;
    [SerializeField] private float rotationSpeed = 400f; 
    [SerializeField] private GameObject[] Orbs;
    private int numOrbs = 1;

    void FixedUpdate()
    {
        Transform objTransform = rotateObject.GetComponent<Transform>();
        objTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        for (int i = 0; i < Orbs.Length; i++)
        {
            Orbs[i].transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }

    public void upgradeOrbs(){
        if (numOrbs < Orbs.Length)
        {
            Orbs[numOrbs].SetActive(true);
            rotationSpeed -= 50f;
            numOrbs += 1;
        }
    }
}
