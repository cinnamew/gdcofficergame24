using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winfred_Skill_Scrambled : MonoBehaviour
{
    [SerializeField] GameObject explosion_prefab;
    public bool unlockedAbility = false;
    private int explosion_damage;
    private Vector3 explosionScale = new Vector3(1, 1, 1);

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
            StartCoroutine(explosion());
        }
    }

    private IEnumerator explosion()
    {
        GameObject explosion = Instantiate(explosion_prefab, transform);
        explosion.transform.localScale = explosionScale;
        explosion.GetComponent<HeroHitbox>().setDamage(explosion_damage);
        yield return new WaitForSeconds(lifeSpan);
        Destroy(explosion);
    }
    public void setCooldown(float num)
    {
        baseProjectileCooldown = num;
    }
    public void setDamage(int num)
    {
        explosion_damage = num;
    }
    public void setExplosionScale(Vector3 s)
    {
        explosionScale = s;
    }
}
