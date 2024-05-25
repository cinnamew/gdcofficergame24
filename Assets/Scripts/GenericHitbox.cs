using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHitbox : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float kbMagnitude;
    [SerializeField] float knockbackDuration;
    [SerializeField] bool autoAttack;
    private Vector2 knockbackVector;
    [SerializeField]private float refreshEvery;
    [SerializeField] private PlayerStats playerStats;
    private float currCooldown;
    public bool isEnabled = true;


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
        int damageDealt = DetermineDamage(target);
        DealDamage(target, damageDealt);
        DealKnockback(target);
        ApplyStatusEffects(target);
    }
    
    int DetermineDamage(Collider2D target){
        float dmg = 1.0f*damage;
        if (target.tag.Equals("Enemy") && playerStats != null){
            if (Random.Range(0f, 100f) < playerStats.Crt){
                dmg = dmg * 1.5f;
            }
            dmg = dmg * playerStats.Atk/100f; //this is really ugly spaghetti code im sorry rohan :(
        }
        return (int)(dmg);
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
        //Debug.Log(triggerList.Count);
        GetComponent<BoxCollider2D>().enabled = true;
        Rigidbody2D rb2d = GetComponentInParent<Rigidbody2D>();
        if(rb2d == null) rb2d = GetComponent<Rigidbody2D>();
        if(rb2d != null) rb2d.WakeUp();//https://forum.unity.com/threads/reenabling-disabled-gameobject-does-not-call-ontriggerenter.765551/ last comment reccomends this as a fix
        //edge case: If the hitbox does not move, refreshing does not work.
        currCooldown = refreshEvery;
    }

    public void EnableHitbox()
    {
        isEnabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponentInParent<Rigidbody2D>().WakeUp(); //https://forum.unity.com/threads/reenabling-disabled-gameobject-does-not-call-ontriggerenter.765551/ last comment reccomends this as a fix
        //edge case: If the hitbox does not move, refreshing does not work.
        currCooldown = refreshEvery;
    }

    public void DisableHitbox()
    {
        isEnabled = false;
        triggerList.Clear();
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void RemoveHitbox()
    {
        Destroy(gameObject);
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

    private void Update()
    {
        if (autoAttack)
        {
            currCooldown -= Time.deltaTime;
            if (currCooldown <= 0) RefreshHitbox();
        }
    }
}