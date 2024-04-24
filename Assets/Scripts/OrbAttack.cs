using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject rotateObject;
    [SerializeField] private float rotationSpeed = 30f; 
    [SerializeField] private Transform[] Orbs;

    void FixedUpdate()
    {
        Transform objTransform = rotateObject.GetComponent<Transform>();
        objTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        for (int i = 0; i < Orbs.Length; i++)
        {
            Orbs[i].Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }
}
