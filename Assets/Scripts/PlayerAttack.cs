using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] bool autoAttack;
    [SerializeField] private GenericHitbox[] hitboxes; //ignore for projectiles because I'm too lazy to do object pooling <3
    [SerializeField] bool isProjectile;
    [SerializeField] float minProjectileCooldown;
    [SerializeField] float lifeSpan;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform projectileSpawnpoint;
    [SerializeField] bool rotateProjectileToAimDir = true;
    private bool isAttacking = true; //toggle attacks; might be good if there's like a character like Diva in Overwatch
    private float timeAttacking; //same reason
    private float timeOfLastAttack; //for manual attacks
    private Vector2 aimDir;

    private void Update()
    {
        if (autoAttack)
        {
            if (isAttacking)
            {
                timeAttacking += Time.deltaTime;
                /*if (isProjectile)
                {
                    if(Time.time - timeOfLastAttack >= minProjectileCooldown) //if it's due time to shoot your shot
                    {
                        timeOfLastAttack = Time.time;
                        Debug.Log("hiqweuiouqwe");
                        StartCoroutine(afterBirthDeathIsInevitable());
                    }
                }*/
                if(Time.time - timeOfLastAttack >= minProjectileCooldown && projectilePrefab != null) //if it's due time to shoot your shot
                {
                    timeOfLastAttack = Time.time;
                    //Debug.Log("hiqweuiouqwe");
                    StartCoroutine(afterBirthDeathIsInevitable());
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
                if (isProjectile)
                {
                    if ((Time.time - timeOfLastAttack) >= minProjectileCooldown) //if it's due time to shoot your shot
                    {
                        StartCoroutine(afterBirthDeathIsInevitable());
                        timeOfLastAttack = Time.time;
                    }
                }
                foreach (GenericHitbox h in hitboxes)
                {
                    h.RefreshHitbox();
                }
            }
        }
    }

    private IEnumerator afterBirthDeathIsInevitable() //handles projectiles
    {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = projectileSpawnpoint.position;
        Debug.Log("aim dir: " + aimDir);
        Debug.Log("aim dir: " + aimDir.normalized);
        if (rotateProjectileToAimDir)
        {
            float angle = Mathf.Atan(aimDir.y / aimDir.x) * (180 / Mathf.PI);
            if (aimDir.x < 0)
            {
                angle += 180;
            }
            projectile.transform.eulerAngles = new Vector3(0, 0, angle);
        }
        if (isProjectile)
        {
            projectile.GetComponent<Rigidbody2D>().velocity = aimDir.normalized * projectileSpeed; //go crazy with this if you wish to make a trapezoidal tornado attack

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
