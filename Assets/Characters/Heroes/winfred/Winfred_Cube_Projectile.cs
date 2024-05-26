using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winfred_Cube_Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dir;
    ParticleSystem ps;

    private float startAngle;
    private float theta; //in radians
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();

        speed = rb.velocity.magnitude;
        startAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        theta = startAngle - Mathf.PI/8;
        rb.velocity = speed * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            ps.gameObject.transform.eulerAngles = new Vector3(0, 0, angle-90);
        }

        theta += Time.deltaTime / 2;

        float x = theta;
        float dx = Mathf.Sin(x) * (-Mathf.Cos(4 * (x-startAngle))) - 4 * Mathf.Sin(4 * (x-startAngle)) * Mathf.Cos(x);
        float dy = Mathf.Cos(x) * Mathf.Cos(4 * (x-startAngle)) - 4 * Mathf.Sin(x) * (Mathf.Sin(4 * (x-startAngle)));
        rb.velocity = speed * new Vector2(dx, dy).normalized;
    }
}
