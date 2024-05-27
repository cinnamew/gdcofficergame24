using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUpgradesManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject spiderCooking;
    void Start()
    {
        
    }

    // Update is called once per frame

    public void Upgrade(UpgradeItem upgrade){
        if (upgrade.itemName == "Spider Cooking")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                spiderCooking.SetActive(true);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                spiderCooking.transform.localScale *= 1.3f;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                spiderCooking.GetComponent<HeroHitbox>().setDamage(13);
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                spiderCooking.transform.localScale *= 1.5f;
            }
            if (upgrade.currentUpgradeLvl == 5)
            {
                spiderCooking.GetComponent<HeroHitbox>().setRefreshEvery(0.75f*0.8f);
            }
            if (upgrade.currentUpgradeLvl == 6)
            {
                spiderCooking.GetComponent<HeroHitbox>().setDamage(19);
            }
            if (upgrade.currentUpgradeLvl == 7)
            {
                spiderCooking.GetComponent<HeroHitbox>().setKnockbackMagnitude(5f);
            }
        }
    }
}
