using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faye_Skill_SocialAnxiety : MonoBehaviour
{
    [SerializeField] GameObject spearCircleHit_prefab;
    public bool unlockedAbility = false;
    private float spearCircleHit_cooldown;
    private int spearCircleHit_damage;

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
            StartCoroutine(spearCircleHit());
        }
    }

    private IEnumerator spearCircleHit()
    {
        GameObject spearCircle = Instantiate(spearCircleHit_prefab, transform);
        HeroHitbox hitbox = spearCircle.GetComponent<HeroHitbox>();
        hitbox.setDamage(spearCircleHit_damage);
        hitbox.setRefreshEvery(spearCircleHit_cooldown); //MIGHT NOT WORK AS INTENDED FIX LATER
        yield return new WaitForSeconds(lifeSpan);
        hitbox.RemoveHitbox();
    }
    public float getCooldown()
    {
        return spearCircleHit_cooldown;
    }
    public float getDamage()
    {
        return spearCircleHit_damage;
    }
    public void setCooldown(float num)
    {
        spearCircleHit_cooldown = num;
    }
    public void setDamage(int num)
    {
        spearCircleHit_damage = num;
    }
}
