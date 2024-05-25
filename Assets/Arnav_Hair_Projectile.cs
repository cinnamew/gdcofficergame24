using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arnav_Hair_Projectile : MonoBehaviour
{
    [SerializeField] GameObject childProjectile;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] float timeBeforeSplitting = 1;
    [SerializeField] float speed = 5;
    [SerializeField] float duration = 5;
    float startTime;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= timeBeforeSplitting)
        {
            if(sprites.Count >= 3)
            {
                float originalAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                gameObject.SetActive(false);
                GameObject p1 = Instantiate(childProjectile, transform.position, transform.rotation);
                p1.GetComponent<SpriteRenderer>().sprite = sprites[0];
                float angle = originalAngle+30;
                p1.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Destroy(p1, duration);

                GameObject p2 = Instantiate(childProjectile, transform.position, transform.rotation);
                p2.GetComponent<SpriteRenderer>().sprite = sprites[1];
                angle = originalAngle;
                p2.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Destroy(p2, duration);
                
                GameObject p3 = Instantiate(childProjectile, transform.position, transform.rotation);
                p3.GetComponent<SpriteRenderer>().sprite = sprites[2];
                angle = originalAngle-30;
                p3.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Destroy(p3, duration);
            }
        }
    }
}
