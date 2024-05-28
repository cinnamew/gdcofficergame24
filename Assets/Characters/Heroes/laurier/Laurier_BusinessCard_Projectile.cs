using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laurier_BusinessCard_Projectile : MonoBehaviour
{
    [SerializeField] GameObject childProjectile;
    private Rigidbody2D rb;
    private List<GameObject> children = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float originalAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        float angle = originalAngle + 120;
        GameObject c1 = Instantiate(childProjectile, transform.position, Quaternion.identity);
        c1.transform.eulerAngles = new Vector3(0, 0, angle);
        c1.GetComponent<Rigidbody2D>().velocity = rb.velocity.magnitude * new Vector2(Mathf.Cos(angle* Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        int dmg = gameObject.GetComponent<HeroHitbox>().getDamage();
        c1.GetComponent<HeroHitbox>().setDamage(dmg);
        children.Add(c1);
        angle = originalAngle - 120;
        GameObject c2 = Instantiate(childProjectile, transform.position, Quaternion.identity);
        c2.transform.eulerAngles = new Vector3(0, 0, angle);
        c2.GetComponent<Rigidbody2D>().velocity = rb.velocity.magnitude * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        c2.GetComponent<HeroHitbox>().setDamage(dmg);
        children.Add(c2);
    }
    private void OnDestroy()
    {
        foreach(GameObject o in children){
            Destroy(o);
        }
    }
}
