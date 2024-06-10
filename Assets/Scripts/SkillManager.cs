using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class SkillManager : MonoBehaviour
{
    [SerializeField] List<UpgradeItem> allSkills = new List<UpgradeItem>();
    [Header("Arnav")]
    private Arnav_Skill_Lightning lightning;

    [Header("Faye")]
    private Faye_Skill_SocialAnxiety spearCircle;

    [Header("Jemian")]
    private Jemi_Skill_Berserk bulletRain;

    [Header("Jolie")]
    private PlayerAttack heartAttack;
    private HeroHitbox heartAttackHitbox;
    private float enemyCheckCooldown = 10f;
    private float enemyCountRange = 5f;
    private float prevEnemyHeal;
    private float enemyDamageRange = 10f;
    private float enemyDamageCooldown = 10f;
    private float enemyDamagePercent = 15f;
    private float prevEnemyDamage;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Lydia")]
    private Lydia_Skill_Flurry flurry;
    
    [Header("Laurier")]
    private float prevCoinDrain;
    private float coinDrainCooldown = 3f;
    private float atkIncrease = 20f;
    private float prevAtkIncrease = 0f;

    [Header("Rohan")]
    private Rohan_Skill_DoubleTrouble doubleTrouble;

    [Header("Vaishak")]
    private Vaishak_Skill_Spin spin;

    [Header("Winfred")]
    private Winfred_Skill_Scrambled scrambled;
    private Winfred_Skill_1x1 decoy;

    private StatsManager statsManager;

    // Start is called before the first frame update
    void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        foreach(UpgradeItem upgrade in allSkills)
        {
            //ARNAV
            if (upgrade.itemName == "Band for Band")
            {
                upgrade.description = "Raises ATK by 8% and CRT by 4% for 4 seconds.";
            }
            else if (upgrade.itemName == "Lightning")
            {
                upgrade.description = "Every 8 seconds, send out a lightning cross beam that deals damage to nearby enemies.";
            }
            //FAYE
            if(upgrade.itemName == "Social Anxiety")
            {
                upgrade.description = "Every 10 seconds, spear swings in a circle, dealing AoE DMG.";
            }
            else if(upgrade.itemName == "Time Crunch")
            {
                upgrade.description = "When Health falls below 20%, increase SPD by 10%, Haste by 10%, ATK by 10%, and CRT by 10% for 10 seconds.";
            }
            else if(upgrade.itemName == "Debugging Hell")
            {
                upgrade.description = "For every enemy defeated, heal 3 HP.";
            }
            //JEMI
            if(upgrade.itemName == "I'll Be Back")
            {
                upgrade.description = "Increases Max HP by 10% and pickup range by 15%.";
            }
            else if(upgrade.itemName == "Fair and Balanced")
            {
                upgrade.description = "Increases CRT rate by 1% every Pellet hit.";
            }
            else if(upgrade.itemName == "Berserk")
            {
                upgrade.description = "Creates a rain of 5 bullets every 10 seconds.";
            }
            //JOLIE
            if(upgrade.itemName == "Heart Attack!")
            {
                upgrade.description = "Every 8 seconds, shoot a heart that explodes into 3 smaller ones in the direction of your cursor.";
            }
            else if(upgrade.itemName == "Empathy")
            {
                upgrade.description = "You heal 0.5% of the damage you deal. Additionally, every " + enemyDamageCooldown + " seconds, the enemy nearest you takes damage equal to " + enemyDamagePercent + "% of the HP you lost.";
            }
            else if(upgrade.itemName == "Enemies to Lovers")
            {
                upgrade.description = "Every " + enemyCheckCooldown + " seconds, for every enemy within a range of 5 meters, there is a 1% chance you will heal to max health.";
            }
            //LAURIER
            if(upgrade.itemName == "Dealing and Wheeling")
            {
                upgrade.description = "Killing an enemy has a 2.5% change to give you 1 coin.";
            }
            else if(upgrade.itemName == "User Fees")
            {
                upgrade.description = "Lose 1 coin every 3 seconds in turn for a 20% ATK buff.";
            }
            //LYDIA
            if(upgrade.itemName == "Flurry")
            {
                upgrade.description = "Shoots out a circle of 5 arrows every 8 seconds.";
            }
            else if(upgrade.itemName == "Sharpshooter")
            {
                upgrade.description = "Increases CRT rate by 10%.";
            }
            //ROHAN
            if(upgrade.itemName == "Swift Snipping")
            {
                upgrade.description = "Increases SPD by 10% and Haste by 20%.";
            }
            else if(upgrade.itemName == "Double Trouble")
            {
                upgrade.description = "Every 9 seconds, projectiles fire out of both sides of the scissors, the one from the handles being slower and weaker.";
            }
            //VAISHAK
            if(upgrade.itemName == "Spin")
            {
                upgrade.description = "Steel ball spins around him and disappears for 3 seconds after 1 spin.";
            }
            else if(upgrade.itemName == "Big Hat")
            {
                upgrade.description = "Increases Max HP by 10% and pickup range by 15%.";
            }
            //WINFRED
            if(upgrade.itemName == "Scrambled")
            {
                upgrade.description = "Every 10 seconds, create an explosion that damages enemies.";
            }
            else if(upgrade.itemName == "R U R' U'")
            {
                upgrade.description = "Increases SPD by 5% and Haste by 20% (b/c its the **** move).";
            }
            else if(upgrade.itemName == "1x1")
            {
                upgrade.description = "Every 10 seconds, deploy a decoy that despawns after 2 seconds.";
            }
        }
    }

    // Update is called once per frame
    void Update(){
        if (Time.time - prevEnemyHeal >= enemyCheckCooldown && GetComponent<UpgradeInventory>().HasItemWithName("Enemies to Lovers")){
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, enemyCountRange, enemyLayer);
            Debug.Log("ENEMIES DETECTED AMOUNT: " + hitColliders.Length);
            if (Random.Range(0, 100) < hitColliders.Length){
                GetComponent<Health>().HealToFull();
                Debug.Log("HEALED TO FULL POGGG");
            }
            prevEnemyHeal = Time.time;
        }
        if (Time.time - prevEnemyDamage >= enemyDamageCooldown && GetComponent<UpgradeInventory>().HasItemWithName("Empathy")){
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, enemyDamageRange, enemyLayer);
            int lowestIndex = 0;
            for  (int i = 1; i < hitColliders.Length; i++){
                float distanceToTarget = Vector2.Distance(transform.position, hitColliders[i].gameObject.transform.position);
                float distanceToTarget2 = Vector2.Distance(transform.position, hitColliders[lowestIndex].gameObject.transform.position);
                if (distanceToTarget < distanceToTarget2){
                    lowestIndex = i;
                }
            }
            if (hitColliders.Length > 0){
                int damageAmt = (int)((statsManager.MaxHp - GetComponent<Health>().GetHealth())*enemyDamagePercent/100f);
                hitColliders[lowestIndex].gameObject.GetComponent<Health>().TakeDamage(damageAmt);
                Debug.Log("DAMANGED ENEMY USING EMPATHY BY : " + damageAmt);
            }
            prevEnemyDamage = Time.time;
        }
        if (Time.time - prevCoinDrain >= coinDrainCooldown && GetComponent<UpgradeInventory>().HasItemWithName("User Fees")){
            bool coinRemoved = Manager.Obj.decreaseNumCoins(1);
            statsManager.Atk -= prevAtkIncrease;
            if (coinRemoved){
                statsManager.Atk += atkIncrease;
                prevAtkIncrease = atkIncrease;
            }
            prevCoinDrain = Time.time;
        }

    }
    
    public void Upgrade(UpgradeItem upgrade){
        //ARNAV
        if (upgrade.itemName == "Band for Band")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.AtkBuff = 8;
                upgrade.CrtBuff = 4;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Raises ATK by 10% and CRT by 6% for 6 seconds."; //for next one
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.AtkBuff = 10;
                upgrade.CrtBuff = 6;
                upgrade.BuffTime = 6;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Raises ATK by 12% and CRT by 8% for 8 seconds.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.AtkBuff = 12;
                upgrade.CrtBuff = 8;
                upgrade.BuffTime = 8;
                upgrade.ApplyBuffs(statsManager);
            }
        }
        else if (upgrade.itemName == "Lightning")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                lightning = gameObject.GetComponent<Arnav_Skill_Lightning>();
                lightning.unlockedAbility = true;
                lightning.setDamage(30);
                lightning.setCooldown(8);
                upgrade.description = "Every 7 seconds, send out a lightning cross beam that deals damage to nearby enemies. Deals 17% more damage than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                lightning.setDamage(35);
                lightning.setCooldown(7);
                upgrade.description = "Every 6 seconds, send out a lightning cross beam that deals damage to nearby enemies. Deals 33% more damage than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                lightning.setDamage(40);
                lightning.setCooldown(6);
            }
        }

        //FAYE
        if (upgrade.itemName == "Social Anxiety"){ //duplicate for other skills
            if (upgrade.currentUpgradeLvl == 1){
                spearCircle = gameObject.GetComponent<Faye_Skill_SocialAnxiety>();
                spearCircle.unlockedAbility = true;
                spearCircle.setDamage(30);
                spearCircle.setCooldown(10);
                upgrade.description = "Every 9 seconds, spear swings in a circle, dealing AoE DMG. Deals 17% more DMG in a 20% larger area than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2){
                spearCircle.setDamage(35);
                spearCircle.setCooldown(9);
                spearCircle.setSpearCircleScale(new Vector2(1.2f, 1.2f));
                upgrade.description = "Every 8 seconds, spear swings in a circle, dealing AoE DMG. Deals 33% more DMG in a 40% larger area than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3){
                spearCircle.setDamage(40);
                spearCircle.setCooldown(8);
                spearCircle.setSpearCircleScale(new Vector2(1.4f, 1.4f));
            }
        }
        else if (upgrade.itemName == "Time Crunch")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.AtkBuff = 10;
                upgrade.SpdBuff = 10;
                upgrade.CrtBuff = 10;
                upgrade.HasteBuff = 10;
                upgrade.BuffTime = 10;
                upgrade.description = "When Health falls below 20%, increase SPD by 10%, Haste by 10%, ATK by 10%, and CRT by 10% for 15 seconds.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.BuffTime = 15;
                upgrade.description = "When Health falls below 20%, increase SPD by 20%, Haste by 20%, ATK by 20%, and CRT by 20% for 15 seconds.";

            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.AtkBuff = 20;
                upgrade.SpdBuff = 20;
                upgrade.CrtBuff = 20;
                upgrade.HasteBuff = 20;
            }
        }
        else if (upgrade.itemName == "Debugging Hell")
        {
            if(upgrade.currentUpgradeLvl == 1)
            {
                upgrade.description = "For every enemy defeated, heal 5 HP.";
            }else if(upgrade.currentUpgradeLvl == 2)
            {
                upgrade.description = "For every enemy defeated, heal 7 HP.";
            }
            
        }

        //JEMI
        if (upgrade.itemName == "I'll Be Back")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.MaxHpBuff = 10;
                upgrade.PurBuff = 15;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases Max HP by 15% and pickup range by 20%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.MaxHpBuff = 15;
                upgrade.PurBuff = 20;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases Max HP by 20% and pickup range by 25%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.MaxHpBuff = 20;
                upgrade.PurBuff = 25;
                upgrade.ApplyBuffs(statsManager);
            }
        }
        else if (upgrade.itemName == "Fair and Balanced")
        {
            //has no upgrade levels, ig too op
        }
        else if (upgrade.itemName == "Berserk")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                bulletRain = gameObject.GetComponent<Jemi_Skill_Berserk>();
                bulletRain.unlockedAbility = true;
                bulletRain.setProjectileDamage(5);
                bulletRain.setCooldown(10);
                upgrade.description = "Creates a rain of 9 bullets every 8 seconds. Deals 60% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                bulletRain.setProjectileDamage(8);
                bulletRain.setCooldown(8);
                bulletRain.setNumProjectiles(9);
                upgrade.description = "Creates a rain of 15 bullets every 6 seconds. Deals 100% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                bulletRain.setProjectileDamage(10);
                bulletRain.setCooldown(6);
                bulletRain.setNumProjectiles(15);
            }
        }

        //JOLIE
        if (upgrade.itemName == "Heart Attack!")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                PlayerAttack[] candidates = GetComponents<PlayerAttack>();
                if (!candidates[0].enabled) heartAttack = candidates[0];
                else heartAttack = candidates[1];
                heartAttack.enabled = true;
                heartAttackHitbox = (HeroHitbox)heartAttack.getHitboxes()[0];
                //set dmg and cd
                heartAttackHitbox.setDamage(30); //dmg must be set w a reference to hero hitbox (the hitbox of the projectile)
                heartAttack.SetBaseProjectileCooldown(8); //cd set w reference to playerattack
                upgrade.description = "Every 7 seconds, shoot a heart that explodes into 3 smaller ones in the direction of your cursor. Deals 17% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                heartAttackHitbox.setDamage(35);
                heartAttack.SetBaseProjectileCooldown(7);
                upgrade.description = "Every 6 seconds, shoot a heart that explodes into 3 smaller ones in the direction of your cursor. Deals 33% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                heartAttackHitbox.setDamage(40);
                heartAttack.SetBaseProjectileCooldown(6);
            }
        }
        else if (upgrade.itemName == "Empathy")
        {
            upgrade.description = "You heal 1% of the damage you deal. Additionally, every 8 seconds, the enemy nearest you takes damage equal to 30% of the HP you lost.";
            if (upgrade.currentUpgradeLvl == 2){
                enemyDamagePercent = 30f;
                enemyDamageCooldown = 8f;
                upgrade.description = "You heal 1.5% of the damage you deal. Additionally, every 5 seconds, the enemy nearest you takes damage equal to 60% of the HP you lost.";
            }
            if (upgrade.currentUpgradeLvl == 3){
                enemyDamagePercent = 60f;
                enemyDamageCooldown = 5f;
            }
        }
        else if (upgrade.itemName == "Enemies to Lovers")
        {
            upgrade.description = "Every 8 seconds, for every enemy within a range of 10 meters, there is a 1% chance you will heal to max health.";
            if (upgrade.currentUpgradeLvl == 2){
                enemyCheckCooldown = 8f;
                enemyCountRange = 10f;
                upgrade.description = "Every 5 seconds, for every enemy within a range of 15 meters, there is a 1% chance you will heal to max health.";
            }
            if (upgrade.currentUpgradeLvl == 3){
                enemyCheckCooldown = 5f;
                enemyCountRange = 15f;
            }
        }

        //LAURIER
        if (upgrade.itemName == "Dealing and Wheeling")
        {
            //upgrades elsewhere
            if(upgrade.currentUpgradeLvl == 1)
            {
                upgrade.description = "Killing an enemy has a 2.5% change to give you 2 coins.";
            }else if(upgrade.currentUpgradeLvl == 2)
            {
                upgrade.description = "Killing an enemy has a 2.5% change to give you 3 coins.";
            }
        }
        else if (upgrade.itemName == "User Fees")
        {
            if (upgrade.currentUpgradeLvl == 1){
                atkIncrease = 20f;
                upgrade.description = "Lose 1 coin every 3 seconds in turn for a 35% ATK buff.";
            }
            if (upgrade.currentUpgradeLvl == 2){
                atkIncrease = 35f;
                upgrade.description = "Lose 1 coin every 6 seconds in turn for a 35% ATK buff.";
            }
            if (upgrade.currentUpgradeLvl == 3){
                coinDrainCooldown = 6f;
            }
        }

        //LYDIA
        if (upgrade.itemName == "Flurry")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                flurry = GetComponent<Lydia_Skill_Flurry>();
                flurry.unlockedAbility = true;
                flurry.setArrowDamage(8);
                flurry.setNumArrows(5);
                flurry.setCooldown(8);
                upgrade.description = "Shoots out a circle of 10 arrows every 7 seconds.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                flurry.setArrowDamage(10);
                flurry.setNumArrows(10);
                flurry.setCooldown(7);
                upgrade.description = "Shoots out a circle of 16 arrows every 6 seconds.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                flurry.setArrowDamage(12);
                flurry.setNumArrows(16);
                flurry.setCooldown(6);
            }
        }
        else if (upgrade.itemName == "Sharpshooter")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.CrtBuff = 10;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases CRT rate by 15%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.CrtBuff = 15;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases CRT rate by 20%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.CrtBuff = 20;
                upgrade.ApplyBuffs(statsManager);
            }
        }

        //ROHAN
        if (upgrade.itemName == "Swift Snipping")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.SpdBuff = 10;
                upgrade.HasteBuff = 20;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases SPD by 15% and Haste by 30%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.SpdBuff = 15;
                upgrade.HasteBuff = 30;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases SPD by 20% and Haste by 40%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.SpdBuff = 20;
                upgrade.HasteBuff = 40;
                upgrade.ApplyBuffs(statsManager);
            }
        }
        else if (upgrade.itemName == "Double Trouble")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                doubleTrouble = GetComponent<Rohan_Skill_DoubleTrouble>();
                doubleTrouble.unlockedAbility = true;
                doubleTrouble.setProjectileDamage(25);
                doubleTrouble.setCooldown(9);
                upgrade.description = "Every 7 seconds, projectiles fire out of both sides of the scissors, the one from the handles being slower and weaker. Deals 20% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                doubleTrouble.setProjectileDamage(30);
                doubleTrouble.setCooldown(7);
                upgrade.description = "Every 5 seconds, projectiles fire out of both sides of the scissors, the one from the handles being slower and weaker. Deals 40% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                doubleTrouble.setProjectileDamage(35);
                doubleTrouble.setCooldown(5);
            }
        }

        //VAISHAK
        if (upgrade.itemName == "Spin")
        {
            SteelBallAttack steelBallAttack = GetComponent<SteelBallAttack>();
            if (upgrade.currentUpgradeLvl == 1)
            {
                steelBallAttack.enabled = true;
                steelBallAttack.SetSteelBallActive();
                steelBallAttack.SetDamage(30);
                steelBallAttack.SetDegreesTillHidden(360f);
                upgrade.description = "Steel ball spins around him and disappears for 3 seconds after 2 spins. Deals 17% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                steelBallAttack.SetDamage(35);
                steelBallAttack.SetDegreesTillHidden(720f);
                upgrade.description = "Steel ball spins around him and disappears for 3 seconds after 3 spins. Deals 33% more DMG than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                steelBallAttack.SetDamage(40);
                steelBallAttack.SetDegreesTillHidden(1080f);
            }
        }
        else if (upgrade.itemName == "Big Hat")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.MaxHpBuff = 10;
                upgrade.PurBuff = 15;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases Max HP by 15% and pickup range by 20%.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.MaxHpBuff = 15;
                upgrade.PurBuff = 20;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases Max HP by 20% and pickup range by 25%.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.MaxHpBuff = 20;
                upgrade.PurBuff = 25;
                upgrade.ApplyBuffs(statsManager);
            }
        }

        //WINFRED
        if (upgrade.itemName == "Scrambled")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                scrambled = gameObject.GetComponent<Winfred_Skill_Scrambled>();
                scrambled.unlockedAbility = true;
                scrambled.setDamage(35);
                scrambled.setCooldown(10);
                upgrade.description = "Every 8 seconds, create an explosion that damages enemies. Deals 14% more DMG over a 20% greater area than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                scrambled.setDamage(40);
                scrambled.setCooldown(8);
                scrambled.setExplosionScale(new Vector3(1.2f, 1.2f, 1f));
                upgrade.description = "Every 6 seconds, create an explosion that damages enemies. Deals 29% more DMG over a 40% greater area than lvl 1.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                scrambled.setDamage(45);
                scrambled.setCooldown(6);
                scrambled.setExplosionScale(new Vector3(1.4f, 1.4f, 1f));
            }
        }
        else if (upgrade.itemName == "R U R' U'")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.SpdBuff = 5;
                upgrade.HasteBuff = 20;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases SPD by 10% and Haste by 30% (b/c its the **** move).";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.SpdBuff = 10;
                upgrade.HasteBuff = 30;
                upgrade.ApplyBuffs(statsManager);
                upgrade.description = "Increases SPD by 15% and Haste by 40% (b/c its the **** move).";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.UnapplyBuffs(statsManager);
                upgrade.SpdBuff = 15;
                upgrade.HasteBuff = 40;
                upgrade.ApplyBuffs(statsManager);
            }
        }
        else if (upgrade.itemName == "1x1")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                decoy = gameObject.GetComponent<Winfred_Skill_1x1>();
                decoy.unlockedAbility = true;
                decoy.setCooldown(10);
                decoy.setDuration(2);
                upgrade.description = "Every 9 seconds, deploy a decoy that despawns after 3 seconds.";
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                decoy.setCooldown(9);
                decoy.setDuration(3);
                upgrade.description = "Every 8 seconds, deploy a decoy that despawns after 4 seconds.";
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                decoy.setCooldown(8);
                decoy.setDuration(4);
            }
        }
    }
}
