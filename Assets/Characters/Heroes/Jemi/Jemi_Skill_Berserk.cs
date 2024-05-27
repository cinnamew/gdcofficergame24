using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jemi_Skill_Berserk : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int projectileDmg;
    [SerializeField] float projectileSpeed;
    public int numProjectilesToSpawn;

    public bool unlockedAbility = false;
    private float minProjectileCooldown;
    [SerializeField] float baseProjectileCooldown;
    [SerializeField] float lifeSpan;
    private float timeOfLastAttack;
    private StatsManager statsManager;
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
        if (unlockedAbility && Time.time - timeOfLastAttack >= minProjectileCooldown) //if it's due time to shoot your shot
        {
            timeOfLastAttack = Time.time;
            Debug.Log("let it rainnnn");
            bulletRain();
        }
    }

    private void bulletRain()
    {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 offset = new Vector3(0, 0, 0);
        float pixelDistanceBetween = Camera.main.pixelWidth / (numProjectilesToSpawn - 1);
        for (int i = 0; i < numProjectilesToSpawn; i++)
        {
            GameObject proj = Instantiate(projectile, topLeft + offset, Quaternion.identity);
            proj.GetComponent<HeroHitbox>().setDamage(projectileDmg);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
            Destroy(proj, lifeSpan);
            offset.x += pixelDistanceBetween;
        }
    }
    public void setProjectileDamage(int d)
    {
        projectileDmg = d;
    }
    public void setNumProjectiles(int n)
    {
        numProjectilesToSpawn = n;
    }
    public void setBaseCooldown(float cd)
    {
        baseProjectileCooldown = cd;
    }
}
