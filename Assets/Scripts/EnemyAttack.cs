using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 0.2f;
    [SerializeField] private string targetTag;
    private float attackTimer = 0f;
    private GameObject target;

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        if (target != null)
        {
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {  
        if (other.gameObject.CompareTag(targetTag))
        {
            target = null;
        }
    }
}
