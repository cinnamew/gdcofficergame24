using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arnav_Skill_Lightning : MonoBehaviour
{
    [SerializeField] GameObject lightningCrossBeam_prefab;
    public bool unlockedAbility = false;
    private float lightningCrossBeam_cooldown;
    private int lightningCrossBeam_damage;

    private float minProjectileCooldown;
    [SerializeField] float baseProjectileCooldown;
    [SerializeField] float lifeSpan;
    private float timeOfLastAttack;
    private StatsManager statsManager;
    private void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    public void SetBaseProjectileCooldown(float cooldown)
    {
        baseProjectileCooldown = cooldown;
    }
    private void Update()
    {
        minProjectileCooldown = baseProjectileCooldown / (1 + statsManager.Haste / 100f);
        if (unlockedAbility && Time.time - timeOfLastAttack >= minProjectileCooldown) //if it's due time to shoot your shot
        {
            timeOfLastAttack = Time.time;
            StartCoroutine(lightningCrossBeam());
        }
    }

    private IEnumerator lightningCrossBeam() 
    {
        GameObject lightning = Instantiate(lightningCrossBeam_prefab, transform.position, Quaternion.identity); ;
        HeroHitbox[] hitboxes = lightning.GetComponentsInChildren<HeroHitbox>();
        Collider2D[] colliders = lightning.GetComponentsInChildren<Collider2D>();
        foreach(HeroHitbox h in hitboxes)
        {
            h.setDamage(lightningCrossBeam_damage);
            h.setRefreshEvery(lightningCrossBeam_cooldown);
        }
        foreach(Collider2D col in colliders)
        {
            //shock every enemy that it touches (it's exactly 2:22 am ;-; and im not sure the best way to do enemy status)
        }
        yield return new WaitForSeconds(lifeSpan);
        foreach (HeroHitbox h in hitboxes)
        {
            h.RemoveHitbox();
        }
    }
    public float getCooldown()
    {
        return lightningCrossBeam_cooldown;
    }
    public float getDamage()
    {
        return lightningCrossBeam_damage;
    }
    public void setCooldown(float num)
    {
        lightningCrossBeam_cooldown = num;
    }
    public void setDamage(int num)
    {
        lightningCrossBeam_damage = num;
    }
}
