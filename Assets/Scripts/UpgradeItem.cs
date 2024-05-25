using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Item", menuName = "Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    public int currentUpgradeLvl;   
    public int maxUpgradeLvl; //if -1 then no max 
    public string itemName;
    public string description;
    public Sprite icon;
    public int MaxHpBuff;
    public float AtkBuff;
    public float SpdBuff;
    public float CrtBuff;
    public float PurBuff;
    public float HasteBuff;
    public bool BuffIsTemporary;

    public enum Type{
        Stat,
        Weapon,
        Skill
    }
    public Type type;
    //UPGRADES
    //Weapon: size/area, damage, hit rate, knockback, number of projectiles    

    public virtual void Reset(){
        currentUpgradeLvl = 1;
    }
    public virtual void Upgrade() {
        //do stuff
        currentUpgradeLvl++;
    }
}
