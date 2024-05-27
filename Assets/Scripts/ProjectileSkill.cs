using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : MonoBehaviour
{
    [SerializeField] bool autoAttack;
    [SerializeField] private GenericHitbox[] hitboxes; //ignore for projectiles because I'm too lazy to do object pooling <3
    [SerializeField] bool isProjectile;
    private float minProjectileCooldown;
    [SerializeField] float baseProjectileCooldown;
    [SerializeField] float lifeSpan;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform projectileSpawnpoint;
    [SerializeField] bool rotateProjectileToAimDir = true;
    [SerializeField] public float spreadAngle = 0;
    [SerializeField] public float spreadRadius = 0;
    [SerializeField] public int projectilesPerShot = 1;
    private bool isAttacking = true; //toggle attacks; might be good if there's like a character like Diva in Overwatch
    private float timeAttacking; //same reason
    private float timeOfLastAttack; //for manual attacks
    private Vector2 aimDir;
    private StatsManager statsManager;
    private void Start(){
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    public void SetBaseProjectileCooldown(float cooldown){
        baseProjectileCooldown = cooldown;
    }
    private void Update()
    {
        minProjectileCooldown = baseProjectileCooldown/(1+statsManager.Haste/100f);
        if (autoAttack)
        {
            if (isAttacking)
            {
                timeAttacking += Time.deltaTime;
                if(Time.time - timeOfLastAttack >= minProjectileCooldown && isProjectile) //if it's due time to shoot your shot
                {
                    timeOfLastAttack = Time.time;
                    //Debug.Log("hiqweuiouqwe");
                    if(projectilesPerShot != 1)
                    {
                        float increment = spreadAngle / (projectilesPerShot - 1);
                        float startingAngle = -spreadAngle / 2;
                        for (int i = 0; i < projectilesPerShot; i++)
                        {
                            float offset = startingAngle + i * increment;
                            StartCoroutine(afterBirthDeathIsInevitable(offset));
                        }
                    }
                    else
                    {
                        StartCoroutine(afterBirthDeathIsInevitable(0));
                    }
                }
                foreach (GenericHitbox h in hitboxes)
                {
                    if (!h.isEnabled)
                    {
                        h.EnableHitbox();
                    }
                }
            }
            else
            {
                timeAttacking = 0;
                foreach (GenericHitbox h in hitboxes)
                {
                    if (!h.isEnabled)
                    {
                        h.DisableHitbox();
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X)) 
            {   
                if (isProjectile && Time.time - timeOfLastAttack >= minProjectileCooldown)
                {
                    timeOfLastAttack = Time.time;
                    if (projectilesPerShot != 1)
                    {
                        float increment = spreadAngle / (projectilesPerShot - 1);
                        float startingAngle = -spreadAngle / 2;
                        for (int i = 0; i < projectilesPerShot; i++)
                        {
                            float offset = startingAngle + i * increment;
                            StartCoroutine(afterBirthDeathIsInevitable(offset));
                        }
                    }
                    else
                    {
                        StartCoroutine(afterBirthDeathIsInevitable(0));
                    }
                }
                foreach (GenericHitbox h in hitboxes)
                {
                    h.RefreshHitbox();
                }
            }
        }
    }

    private IEnumerator afterBirthDeathIsInevitable(float offsetAngle) //handles projectiles
    {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = projectileSpawnpoint.position;
        if (rotateProjectileToAimDir)
        {
            float angle = Mathf.Atan(aimDir.y / aimDir.x) * (180 / Mathf.PI);
            if (aimDir.x < 0)
            {
                angle += 180;
            }
            projectile.transform.eulerAngles = new Vector3(0, 0, angle + offsetAngle);
        }
        if (isProjectile)
        {
            projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.right * projectileSpeed; //go crazy with this if you wish to make a trapezoidal tornado attack

        }
        else
        {
            projectile.transform.parent = transform;
        }
        yield return new WaitForSeconds(lifeSpan);
        projectile.GetComponent<GenericHitbox>().RemoveHitbox();
    }

    public void setAimDir(Vector2 dir)
    {
        aimDir = dir; //do this from player aiming
    }
}
