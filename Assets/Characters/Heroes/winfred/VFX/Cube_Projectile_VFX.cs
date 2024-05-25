using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Projectile_VFX : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dir;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = rb.velocity.normalized;
        if(dir.x != 0 || dir.y != 0)
        {
            float angle = Mathf.Atan(dir.y / dir.x) * (180 / Mathf.PI);
            if (dir.x < 0)
            {
                angle += 180;
            }
            angle -= ps.shape.arc / 2;
            angle += 180;
            //Debug.Log(angle);
            ps.gameObject.transform.eulerAngles = new Vector3(0, 0, angle-90);
        }
    }
}
