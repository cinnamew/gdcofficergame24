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
    public void Upgrade(UpgradeItem upgrade)
    {
        if (gameObject.name.Contains("Arnav"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                //previous value: 30
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(35);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.85f);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(40);
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.7f);
            }
        }

        if (gameObject.name.Contains("Faye"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                //previous value: 30
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(40);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.75f);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().setProjectileScale(new Vector3(1.2f, 1.2f, 1f));
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(50);
            }
        }

        if (gameObject.name.Contains("Jemi"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                GenericHitbox[] hb = gameObject.GetComponent<PlayerAttack>().getHitboxes();
                foreach (GenericHitbox h in hb)
                {
                    h.setDamage(15); //prev: 10
                }
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.75f);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().projectilesPerShot = 5; //prev: 3
                if (upgrade.currentUpgradeLvl == 4)
                {
                    GenericHitbox[] hb = gameObject.GetComponent<PlayerAttack>().getHitboxes();
                    foreach (GenericHitbox h in hb)
                    {
                        h.setDamage(20); //prev: 15
                    }
                    gameObject.GetComponent<PlayerAttack>().projectilesPerShot = 8;
                }
            }
        }
        Debug.Log("PLEASE WORK PT 3 PLEASEEEEEEEEEE");
        if (gameObject.name.Contains("Jolie"))
        {
            Debug.Log("PLEASE WORK PT 2 PLEASEEEEEEEEEE");
            GetComponent<OrbAttack>().upgradeOrbs();
        }

        if (gameObject.name.Contains("Laurier"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(20); //prev: 15
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1.4f); //prev: 2
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(25); //prev: 20
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.8f); //prev: 1.5f
            }
        }

        if (gameObject.name.Contains("Lydia"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(30); //prev: 25
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.8f); //prev: 1
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(35); //prev: 30
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.6f); //prev: 0.8f
            }
        }

        if (gameObject.name.Contains("Rohan"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().setProjectileScale(new Vector3(1.5f, 1.5f, 1f)); 
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(35); //prev: 30
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().setProjectileScale(new Vector3(2f, 2f, 1f));
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.8f); //prev: 1
            }
        }

        if (gameObject.name.Contains("Vaishak"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(35); //prev: 30
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1.5f); //prev: 2
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(40); //prev: 35
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1f); //prev: 1
            }
        }

        if (gameObject.name.Contains("Winfred"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(4f);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(2f);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(4f / 3f);
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1f);
            }
        }
        
    }
}