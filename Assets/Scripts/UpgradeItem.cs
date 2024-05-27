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
    public float BuffTime;

    public bool IsWeapon;
    public bool IsSkill;
    //UPGRADES
    //Weapon: size/area, damage, hit rate, knockback, number of projectiles    

    void Start(){
        Reset();
    }
    public virtual void Reset(){
        currentUpgradeLvl = 1;
        if (IsWeapon){
            currentUpgradeLvl = 2;
        }
    }
    public virtual void Upgrade() {
        //do stuff
        currentUpgradeLvl++;
    }

    public void ApplyBuffs(StatsManager statsManager){ //should probably not include this as just a param
        statsManager.MaxHp += MaxHpBuff;
        statsManager.Atk += AtkBuff;
        statsManager.Spd += SpdBuff;
        statsManager.Crt += CrtBuff;
        statsManager.Pur += PurBuff;
        statsManager.Haste += HasteBuff;
    }

    public void UnapplyBuffs(StatsManager statsManager){
        statsManager.MaxHp -= MaxHpBuff;
        statsManager.Atk -= AtkBuff;
        statsManager.Spd -= SpdBuff;
        statsManager.Crt -= CrtBuff;
        statsManager.Pur -= PurBuff;
        statsManager.Haste -= HasteBuff;
    }
}
