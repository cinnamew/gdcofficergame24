
using System.Collections.Generic;
using UnityEngine;

public class GenericHitbox : MonoBehaviour
{
    int damage;
    Vector2 knockbackVector;

    private List<Collider2D> triggerList = new List<Collider2D>();
    public string targetTag; //could be enemy or hero. For testing purposes

    void OnTriggerStay2D(Collider2D target) //so apparently this only works if the target has a rigidbody
    {
        if (target.tag.Equals(targetTag)) {
            if (!triggerList.Contains(target)) { //if the list doesn't already contain the target
                OnHit(target); //does a bunch of stuff, please override :)
                triggerList.Add(target);
                Debug.Log(triggerList.Count + " targets within hitbox");
                //This is NOT hit-per-frame!!!
            }

        }
        //if you want the same attack to hit multiple times, you can just clear the list or spawn a new hitbox.
    }

    void OnHit(Collider2D target) {
        DealDamage(target, damage);
        DealKnockback(target);
    }
    
    void DealDamage(Collider2D target, int damageVal) {
        target.GetComponent<Health>().TakeDamage(damageVal);
    }

    void DealKnockback(Collider2D target) {
        target.GetComponent<Rigidbody2D>().AddForce(knockbackVector);
    }

    [ContextMenu("Reset the Hitbox")]
    void RefreshHitbox() {
        GetComponent<Collider2D>().enabled = false;
        triggerList.Clear();
        GetComponent<Collider2D>().enabled = true;
    }

    void ApplyStatusEffects() {

    }
    
}
