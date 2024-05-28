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
        if (gameObject.name.Contains("Arnav")){
            //Do something
        }
        if (gameObject.name.Contains("Faye")){
            //Do something
        }
        if (gameObject.name.Contains("Jemi")){
            //Do something
        }
        if (gameObject.name.Contains("Jolie")){
            gameObject.GetComponent<OrbAttack>().upgradeOrbs();
        }
        if (gameObject.name.Contains("Laurier")){
            //Do something
        }
        if (gameObject.name.Contains("Lydia")){
            //Do something
        }
        if (gameObject.name.Contains("Rohan")){
            //Do something
        }
        if (gameObject.name.Contains("Vaishak")){
            //Do something
        }
        if (gameObject.name.Contains("Winfred")){
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