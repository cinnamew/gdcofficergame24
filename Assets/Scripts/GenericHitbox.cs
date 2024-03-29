using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHitbox : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float kbMagnitude;
    [SerializeField] float knockbackDuration;
    private Vector2 knockbackVector;
    [SerializeField]private float refreshEvery;
     float currCooldown;

    private List<Collider2D> triggerList = new List<Collider2D>();
    public string targetTag; //could be enemy or hero

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag.Equals(targetTag)) {
            if (!triggerList.Contains(target)) {
                OnHit(target); //does a bunch of stuff, please override :)
                triggerList.Add(target);
                Debug.Log(triggerList.Count + " targets within hitbox");
                //This is NOT hit-per-frame
            }
        }
    }



    void OnHit(Collider2D target) {
        DealDamage(target, damage);
        DealKnockback(target);
        ApplyStatusEffects(target);
    }
    
    void DealDamage(Collider2D target, int damageVal) {
        target.GetComponent<Health>().TakeDamage(damageVal);
    }

    void DealKnockback(Collider2D target) {
        knockbackVector = kbMagnitude * (target.transform.position - transform.position);
        target.GetComponent<Rigidbody2D>().AddForce(knockbackVector);
        StartCoroutine(KnockbackTimer(target));
    }

    [ContextMenu("Reset the Hitbox")]
     public void RefreshHitbox() {
        Debug.Log("Refreshing the hitbox");
        triggerList.Clear();
        GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log(triggerList.Count);
        GetComponent<BoxCollider2D>().enabled = true;
        //edge case: If the hitbox does not move, refreshing does not work.
        currCooldown = refreshEvery;
    }

    // IEnumerator EnableHitBox(float waitTime) { //for debugging
    //     yield return new WaitForSeconds(waitTime);
    //     GetComponent<BoxCollider2D>().enabled = true;
    // }

    void ApplyStatusEffects(Collider2D target) {
        
    }

    IEnumerator KnockbackTimer(Collider2D target)
    {
        EnemyMovement em = target.gameObject.GetComponent<EnemyMovement>();
        if (em != null) em.SetCanMove(false);
        yield return new WaitForSeconds(knockbackDuration);
        if (em != null) em.SetCanMove(true);
    }
}