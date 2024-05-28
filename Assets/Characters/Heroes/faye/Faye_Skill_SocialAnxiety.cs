using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faye_Skill_SocialAnxiety : MonoBehaviour
{
    [SerializeField] GameObject spearCircleHit_prefab;
    public bool unlockedAbility = false;
    private float spearCircleHit_cooldown;
    private int spearCircleHit_damage;
    private Vector3 spearCircleHit_scale = new Vector3(1, 1, 1);

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
        spearCircle.transform.localScale = spearCircleHit_scale;
        HeroHitbox hitbox = spearCircle.GetComponent<HeroHitbox>();
        hitbox.setDamage(spearCircleHit_damage);
        //hitbox.setRefreshEvery(spearCircleHit_cooldown); //BROKEN, FIX LATER
        yield return new WaitForSeconds(lifeSpan);
        hitbox.RemoveHitbox();
    }
    public void setCooldown(float num)
    {
        baseProjectileCooldown = num;
    }
    public void setDamage(int num)
    {
        spearCircleHit_damage = num;
    }
    public void setSpearCircleScale(Vector3 s)
    {
        spearCircleHit_scale = s;
    }
}
