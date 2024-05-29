using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] Vector3 ogPosition;
    [SerializeField] float offset = 3f;
    [SerializeField] float sinOffset = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        ogPosition = transform.position;
        //Debug.Log(ogPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ogPosition + offset*Vector3.up*Mathf.Sin(sinOffset*Time.time);
        //print(Mathf.Sin(Time.time));
    }

    void Reset() {
        transform.position = ogPosition;
    }
}
