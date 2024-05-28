using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaishak_Skill_Spin : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDmg;
    [SerializeField] float projectileSpeed;
    [SerializeField] float radius;

    public bool unlockedAbility = false;
    private float minProjectileCooldown;
    [SerializeField] float baseProjectileCooldown;
    [SerializeField] float lifeSpan;
    private float timeOfLastAttack;
    private StatsManager statsManager;

    Rigidbody2D activeProjectileRB;
    private float theta;

    // Start is called before the first frame update
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
        if (unlockedAbility && activeProjectileRB == null && Time.time - timeOfLastAttack >= minProjectileCooldown) //if it's due time to shoot your shot
        {
            timeOfLastAttack = Time.time;
            spin();
        }
        if(activeProjectileRB != null)
        {
            if(Time.time - timeOfLastAttack >= lifeSpan) //time to remove proj
            {
                Destroy(activeProjectileRB.gameObject);
                activeProjectileRB = null;
                theta = 0;
            }
            else
            {
                theta +=  Time.deltaTime;
                Debug.Log("theta: " + theta);

                float dx = radius * -Mathf.Sin(theta);
                float dy = radius * Mathf.Cos(theta);
                activeProjectileRB.velocity = projectileSpeed * new Vector2(dx, dy).normalized;
                Debug.Log(activeProjectileRB.velocity);
            }
        }
    }

    private void spin()
    {
        theta = 0;
        GameObject proj = Instantiate(projectile, transform);
        proj.GetComponent<Vaishak_Ball_Projectile>().enabled = false;
        proj.GetComponent<HeroHitbox>().setDamage(projectileDmg);
        activeProjectileRB = proj.GetComponent<Rigidbody2D>();
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
