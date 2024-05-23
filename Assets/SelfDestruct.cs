using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float delay = 2;
    float originalTime;
    // Start is called before the first frame update
    void Start()
    {
        originalTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - originalTime >= delay)
        {
            Destroy(this.gameObject);
        }
    }
}
