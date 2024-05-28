using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jolie_Skill_HeartAttack : MonoBehaviour
{
    [SerializeField] GameObject childProjectile;
    [SerializeField] float timeBeforeSplitting = 1;
    private float speed;
    private int damage;
    [SerializeField] float duration = 5;
    float startTime;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        speed = rb.velocity.magnitude;
        damage = (int)(GetComponent<HeroHitbox>().getDamage() * 0.8f);

    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= timeBeforeSplitting)
        {
            float originalAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            gameObject.SetActive(false);
            GameObject p1 = Instantiate(childProjectile, transform.position, transform.rotation);
            float angle = originalAngle + 30;
            p1.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            p1.GetComponent<HeroHitbox>().setDamage(damage);
            Destroy(p1, duration);

            GameObject p2 = Instantiate(childProjectile, transform.position, transform.rotation);
            angle = originalAngle;
            p2.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            p2.GetComponent<HeroHitbox>().setDamage(damage);
            Destroy(p2, duration);

            GameObject p3 = Instantiate(childProjectile, transform.position, transform.rotation);
            angle = originalAngle - 30;
            p3.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            p3.GetComponent<HeroHitbox>().setDamage(damage);
            Destroy(p3, duration);
        }
    }
}
