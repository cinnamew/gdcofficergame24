using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegateRotation : MonoBehaviour
{
    [SerializeField] Transform rohanTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newRot = -rohanTransform.eulerAngles.z;
        transform.localEulerAngles = new Vector3(0, 0, newRot);
    }
}
