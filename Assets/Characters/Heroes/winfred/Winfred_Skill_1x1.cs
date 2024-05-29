using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winfred_Skill_1x1 : MonoBehaviour
{
    [SerializeField] GameObject decoy;
    public bool unlockedAbility = false;
    [SerializeField] float duration;

    private float minAbilityCooldown;
    [SerializeField] float baseAbilityCooldown;
    private float timeOfLastSpawn;
    private StatsManager statsManager;
    private void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    private void Update()
    {
        minAbilityCooldown = baseAbilityCooldown / (1 + statsManager.Haste / 100f);
        if (unlockedAbility && Time.time - timeOfLastSpawn >= minAbilityCooldown) //if it's due time to shoot your shot
        {
            timeOfLastSpawn = Time.time;
            StartCoroutine(spawnDecoy());
        }
    }

    private IEnumerator spawnDecoy()
    {
        GameObject dec = Instantiate(decoy, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(duration);
        Destroy(dec);
    }
    public void setCooldown(float num)
    {
        baseAbilityCooldown = num;
    }
    public void setDuration(float t)
    {
        duration = t;
    }
}
