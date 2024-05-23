using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Item", menuName = "Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    public int currentUpgradeLvl;   
    public int maxUpgradeLvl; //if -1 then no max 
    public string itemName;
    public string description;
    public Sprite icon;
    //UPGRADES
    //Weapon: size/area, damage, hit rate, knockback, number of projectiles    

    void Upgrade() {
        //do stuff
    }
}
