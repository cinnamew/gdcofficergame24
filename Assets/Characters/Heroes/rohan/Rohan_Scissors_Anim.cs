using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rohan_Scissors_Anim : MonoBehaviour //this whole script is really scuffed :p
{
    Vector3 offset; //offset from 0
    public float amplitude = 0.01f;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.Translate(-offset, Space.World);
            offset = new Vector3(0, Mathf.Sin(Time.time*speed) * amplitude, 0);
            transform.Translate(offset, Space.World);
        }
        else
        {
            transform.Translate(-offset, Space.World);
            offset = new Vector3(0, 0, 0);
        }
    }
}
