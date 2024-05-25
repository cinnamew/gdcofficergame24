using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winfred_Cube_Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dir;
    ParticleSystem ps;

    Vector2 startingPosition;
    private float theta; //in radians
    private float speed;
    private float startAngle; //in radians
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();

        startingPosition = transform.position;
        speed = rb.velocity.magnitude;
        startAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);

        theta = -Mathf.PI / 8;
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
            ps.gameObject.transform.eulerAngles = new Vector3(0, 0, angle-90);
        }

        Vector2 r = (Vector2)transform.position - startingPosition;
        theta += Time.deltaTime;
        
        float x = theta;
        float dx = Mathf.Sin(x) * (-Mathf.Cos(4*x)) - 4 * Mathf.Sin(4*x) * Mathf.Cos(x);
        float dy = Mathf.Cos(x) * Mathf.Cos(4 * x) - 4 * Mathf.Sin(x) * (Mathf.Sin(4 * x));
        rb.velocity = speed * new Vector2(dx, dy).normalized;
    }
}
