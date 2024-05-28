using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rohan_Skill_DoubleTrouble : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDmg;
    [SerializeField] float projectileSpeed;

    public bool unlockedAbility = false;
    private float minProjectileCooldown;
    [SerializeField] float baseProjectileCooldown;
    [SerializeField] float lifeSpan;
    private float timeOfLastAttack;
    private StatsManager statsManager;

    private PlayerAttack pa;
    // Start is called before the first frame update
    private void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        pa = GetComponent<PlayerAttack>();
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
            doubleTrouble();
        }
    }

    private void doubleTrouble()
    {
        GameObject proj1 = Instantiate(projectile, transform.position, Quaternion.identity);
        proj1.GetComponent<HeroHitbox>().setDamage(projectileDmg);
        proj1.GetComponent<Rigidbody2D>().velocity = projectileSpeed * pa.getAimDir();
        Destroy(proj1, lifeSpan);

        GameObject proj2 = Instantiate(projectile, transform.position, Quaternion.identity);
        proj2.GetComponent<HeroHitbox>().setDamage((int)(projectileDmg*0.6f));
        proj2.GetComponent<Rigidbody2D>().velocity = -projectileSpeed*0.6f * pa.getAimDir();
        Destroy(proj2, lifeSpan);

    }
    public void setProjectileDamage(int d)
    {
        projectileDmg = d;
    }
    public void setCooldown(float cd)
    {
        baseProjectileCooldown = cd;
    }
}
