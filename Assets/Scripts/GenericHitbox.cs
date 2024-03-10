
using System.Collections.Generic;
using UnityEngine;

public class GenericHitbox : MonoBehaviour
{
    int damage;
    Vector2 knockbackVector;

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
        //if you want the same attack to hit multiple times, you can just reset the hitbox or spawn a new one.
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
        target.GetComponent<Rigidbody2D>().AddForce(knockbackVector);
    }

    [ContextMenu("Reset the Hitbox")]
    void RefreshHitbox() {
        triggerList.Clear();
    }

    void ApplyStatusEffects(Collider2D target) {
        
    }
    
}
