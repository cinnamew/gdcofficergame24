using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    private StatsManager statsManager;
    void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    // Update is called once per frame
    public void Upgrade(UpgradeItem upgrade){
        if (gameObject.name.Contains("Jolie")){
            gameObject.GetComponent<OrbAttack>().upgradeOrbs();
        } else if (gameObject.name.Contains("Jemi")){
            gameObject.GetComponent<OrbAttack>().upgradeOrbs();
        } else if (gameObject.name.Contains("Winfred")){
            if (upgrade.currentUpgradeLvl == 1){
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(4f);
            }
            if (upgrade.currentUpgradeLvl == 2){
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(2f);
            }
            if (upgrade.currentUpgradeLvl == 3){
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(4f/3f);
            }
            if (upgrade.currentUpgradeLvl == 4){
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1f);
            }
        }
    }
}