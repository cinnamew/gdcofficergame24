using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaishak_Ball_Projectile : MonoBehaviour
{
    //this script just makes the projectile go in a spiral
    Vector2 startingPosition;
    private float theta; //in radians
    private Rigidbody2D rb;
    private float speed;
    private float startAngle; //in radians

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        speed = rb.velocity.magnitude;
        startAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 r = (Vector2)transform.position - startingPosition;
        theta = r.magnitude + startAngle;

        float dy = (theta-startAngle) * Mathf.Cos(theta) + Mathf.Sin(theta);
        float dx = -(theta-startAngle) * Mathf.Sin(theta) + Mathf.Cos(theta);
        rb.velocity = speed * new Vector2(dx, dy).normalized;
    }
}
