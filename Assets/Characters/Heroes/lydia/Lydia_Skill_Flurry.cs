using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lydia_Skill_Flurry : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowSpeed;
    [SerializeField] float radius;
    [SerializeField] int arrowDmg;
    public int numArrowsToSpawn;
    [SerializeField] float lifeSpan;

    public bool unlockedAbility = false;
    private float minAbilityCooldown;
    [SerializeField] float baseAbilityCooldown;
    private float timeOfLastAttack;
    private StatsManager statsManager;
    // Start is called before the first frame update
    private void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    public void SetBaseProjectileCooldown(float cooldown)
    {
        baseAbilityCooldown = cooldown;
    }
    private void Update()
    {
        minAbilityCooldown = baseAbilityCooldown / (1 + statsManager.Haste / 100f);
        if (unlockedAbility && Time.time - timeOfLastAttack >= minAbilityCooldown) //if it's due time to shoot your shot
        {
            timeOfLastAttack = Time.time;
            arrowCircle();
        }
    }

    private void arrowCircle()
    {
        float degreesBetweenArrows = 360 / (numArrowsToSpawn);
        for (int i = 0; i < numArrowsToSpawn; i++)
        {
            Vector3 pos = new Vector3(Mathf.Cos(i*degreesBetweenArrows * Mathf.Deg2Rad), Mathf.Sin(i*degreesBetweenArrows * Mathf.Deg2Rad), 0);
            GameObject proj = Instantiate(arrow, transform.position + pos, Quaternion.identity);
            proj.transform.eulerAngles = new Vector3(0, 0, i * degreesBetweenArrows);
            proj.GetComponent<HeroHitbox>().setDamage(arrowDmg);
            proj.GetComponent<Rigidbody2D>().velocity = arrowSpeed * pos.normalized;
            Destroy(proj, lifeSpan);
        }
    }
    public void setArrowDamage(int d)
    {
        arrowDmg = d;
    }
    public void setNumArrows(int n)
    {
        numArrowsToSpawn = n;
    }
    public void setCooldown(float cd)
    {
        baseAbilityCooldown = cd;
    }
}
