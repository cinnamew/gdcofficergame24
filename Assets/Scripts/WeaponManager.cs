using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    private StatsManager statsManager;
    [SerializeField] UpgradeItem weapon;
    void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        if (gameObject.name.Contains("Arnav"))
        {
            weapon.description = "Deals 17% more DMG.";
        }

        if (gameObject.name.Contains("Faye"))
        {
            weapon.description = "Deals 33% more DMG.";
        }

        if (gameObject.name.Contains("Jemi"))
        {
            weapon.description = "Deals 50% more DMG";
        }
        if (gameObject.name.Contains("Jolie"))
        {
            weapon.description = "Gain an additional orbiting heart.";
        }

        if (gameObject.name.Contains("Laurier"))
        {
            weapon.description = "Deals 33% more DMG.";
        }

        if (gameObject.name.Contains("Lydia"))
        {
            weapon.description = "Deals 20% more DMG.";
        }

        if (gameObject.name.Contains("Rohan"))
        {
            weapon.description = "Increases AoE by 50%.";
        }

        if (gameObject.name.Contains("Vaishak"))
        {
            weapon.description = "Deals 17% more DMG.";
        }

        if (gameObject.name.Contains("Winfred"))
        {
            weapon.description = "Decreases cooldown by 25%.";
        }
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
                upgrade.description = "Decreases cooldown by 15%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.85f);
                upgrade.description = "Deals 33% more DMG.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(40);
                upgrade.description = "Decreases cooldown by 30%.";
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
                upgrade.description = "Decreases cooldown by 25%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.75f);
                upgrade.description = "Increases AoE by 20%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().setProjectileScale(new Vector3(1.2f, 1.2f, 1f));
                upgrade.description = "Deals 66% more DMG.";
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
                upgrade.description = "Decreases cooldown by 25%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.75f);
                upgrade.description = "Shoot one more Pellet per round.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().projectilesPerShot = 10; //prev: 9
                upgrade.description = "Deals 100% more DMG. Shoot 2 more Pellets per round.";
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                GenericHitbox[] hb = gameObject.GetComponent<PlayerAttack>().getHitboxes();
                foreach (GenericHitbox h in hb)
                {
                    h.setDamage(20); //prev: 15
                }
                gameObject.GetComponent<PlayerAttack>().projectilesPerShot = 11;
            }
        }
        if (gameObject.name.Contains("Jolie"))
        {
            GetComponent<OrbAttack>().upgradeOrbs();
            //upgrading is just getting one more orb so dont need to change desc
        }

        if (gameObject.name.Contains("Laurier"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(20); //prev: 15
                upgrade.description = "Decrease cooldown by 30%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1.4f); //prev: 2
                upgrade.description = "Deals 66% more DMG.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(25); //prev: 20
                upgrade.description = "Decrease cooldown by 60%.";
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.8f); //prev: 1.4f
            }
        }

        if (gameObject.name.Contains("Lydia"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(30); //prev: 25
                upgrade.description = "Decrease cooldown by 20%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(0.8f); //prev: 1
                upgrade.description = "Deals 40% more DMG.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(35); //prev: 30
                upgrade.description = "Decrease cooldown by 40%.";
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
                upgrade.description = "Deal 17% more DMG.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(35); //prev: 30
                upgrade.description = "Increase AoE by 100%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().setProjectileScale(new Vector3(2f, 2f, 1f));
                upgrade.description = "Decreases cooldown by 20%.";
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
                upgrade.description = "Decrease cooldown by 25%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1.5f); //prev: 2
                upgrade.description = "Deal 33% more DMG.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().getHitboxes()[0].setDamage(40); //prev: 35
                upgrade.description = "Decrease cooldown by 50%.";
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1f); //prev: 1.5
            }
        }

        if (gameObject.name.Contains("Winfred"))
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(3f);
                upgrade.description = "Decrease cooldown by 50%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(2f);
                upgrade.description = "Decrease cooldown by 66%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(4f / 3f);
                upgrade.description = "Decrease cooldown by 75%.";
            }
            if (upgrade.currentUpgradeLvl == 4)
            {
                gameObject.GetComponent<PlayerAttack>().SetBaseProjectileCooldown(1f);
            }
        }
        
    }
}