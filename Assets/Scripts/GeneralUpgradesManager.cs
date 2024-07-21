using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUpgradesManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject spiderCooking;
    [SerializeField] private GameObject blBook;
    void Start()
    {
        
    }

    // Update is called once per frame

    //kudos to cog539 on the MelonJam discord
    public void Upgrade(UpgradeItem upgrade) {
        switch (upgrade.currentUpgradeLvl) {
            case 1:
                spiderCooking.GetComponent<HeroHitbox>().setRefreshEvery(1f);
                spiderCooking.SetActive(true);
                break;
            case 2:
                spiderCooking.transform.localScale *= 1.3f;
                break;
            case 3:
                spiderCooking.GetComponent<HeroHitbox>().setDamage(10); 
                break;
            case 4:
                spiderCooking.transform.localScale *= 1.5f;
                break;
            case 5:
                spiderCooking.GetComponent<HeroHitbox>().setRefreshEvery(0.8f);
                break;
            case 6:
                spiderCooking.GetComponent<HeroHitbox>().setDamage(15);
                break;
            case 7:
                spiderCooking.GetComponent<HeroHitbox>().setKnockbackMagnitude(25f);
                break;
        }
        if (upgrade.itemName == "AP Prep Book"){
            if (upgrade.currentUpgradeLvl == 1)
            {
                blBook.SetActive(true);
            }
            blBook.GetComponent<BLBookAttack>().upgradeBooks();
        }

    }

}
