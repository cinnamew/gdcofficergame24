using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
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
    private float enemyDamagePercent = 25f;
    private float prevEnemyDamage;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Lydia")]
    private Lydia_Skill_Flurry flurry;
    
    [Header("Laurier")]
    private float prevCoinDrain;
    private float coinDrainCooldown = 5f;
    private float atkIncrease = 50f;
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
        if (Time.time - prevEnemyDamage >= enemyCheckCooldown && GetComponent<UpgradeInventory>().HasItemWithName("Empathy")){
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
                upgrade.AtkBuff = 10;
                upgrade.CrtBuff = 5;
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.AtkBuff = 12;
                upgrade.CrtBuff = 8;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.AtkBuff = 15;
                upgrade.CrtBuff = 10;
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
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                lightning.setDamage(40);
                lightning.setCooldown(6);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                lightning.setDamage(50);
                lightning.setCooldown(5);
            }
        }

        //FAYE
        if (upgrade.itemName == "Social Anxiety"){ //duplicate for other skills
            if (upgrade.currentUpgradeLvl == 1){
                spearCircle = gameObject.GetComponent<Faye_Skill_SocialAnxiety>();
                spearCircle.unlockedAbility = true;
                spearCircle.setDamage(30);
                spearCircle.setCooldown(10);
            }
            if (upgrade.currentUpgradeLvl == 2){
                spearCircle.setDamage(35);
                spearCircle.setCooldown(8);
                spearCircle.setSpearCircleScale(new Vector2(1.25f, 1.25f));
            }
            if (upgrade.currentUpgradeLvl == 3){
                spearCircle.setDamage(40);
                spearCircle.setCooldown(6);
                spearCircle.setSpearCircleScale(new Vector2(1.5f, 1.5f));
            }
        }
        else if (upgrade.itemName == "Time Crunch")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.AtkBuff = 20;
                upgrade.SpdBuff = 20;
                upgrade.CrtBuff = 20;
                upgrade.HasteBuff = 20;
                upgrade.BuffTime = 3;
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.BuffTime = 5;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.AtkBuff = 30;
                upgrade.SpdBuff = 30;
                upgrade.CrtBuff = 30;
                upgrade.HasteBuff = 30;
            }
        }
        else if (upgrade.itemName == "Debugging Hell")
        {
            //upgrades in health script istead
        }

        //JEMI
        if (upgrade.itemName == "I'll Be Back")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.MaxHpBuff = 10;
                upgrade.PurBuff = 15;
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.MaxHpBuff = 15;
                upgrade.PurBuff = 20;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.MaxHpBuff = 20;
                upgrade.PurBuff = 25;
            }
        }
        else if (upgrade.itemName == "Fair and Balanced")
        {
            //upgrades elsewhere
        }
        else if (upgrade.itemName == "Berserk")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                bulletRain = gameObject.GetComponent<Jemi_Skill_Berserk>();
                bulletRain.unlockedAbility = true;
                bulletRain.setProjectileDamage(10);
                bulletRain.setCooldown(10);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                bulletRain.setProjectileDamage(12);
                bulletRain.setCooldown(8);
                bulletRain.setNumProjectiles(9);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                bulletRain.setProjectileDamage(15);
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
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                heartAttackHitbox.setDamage(40);
                heartAttack.SetBaseProjectileCooldown(6);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                heartAttackHitbox.setDamage(50);
                heartAttack.SetBaseProjectileCooldown(4);
            }
        }
        else if (upgrade.itemName == "Empathy")
        {
            if (upgrade.currentUpgradeLvl == 2){
                enemyDamagePercent = 50f;
                enemyDamageCooldown = 8f;
            }
            if (upgrade.currentUpgradeLvl == 3){
                enemyDamagePercent = 100f;
                enemyDamageCooldown = 5f;
            }
        }
        else if (upgrade.itemName == "Enemies to Lovers")
        {
            if (upgrade.currentUpgradeLvl == 2){
                enemyCheckCooldown = 8f;
                enemyCountRange = 10f;
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
        }
        else if (upgrade.itemName == "User Fees")
        {
            if (upgrade.currentUpgradeLvl == 1){
                atkIncrease = 50f;
                atkIncrease = 10f;
            }
            if (upgrade.currentUpgradeLvl == 2){
                atkIncrease = 100f;
            }
            if (upgrade.currentUpgradeLvl == 3){
                coinDrainCooldown = 10f;
            }
        }

        //LYDIA
        if (upgrade.itemName == "Flurry")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                flurry = GetComponent<Lydia_Skill_Flurry>();
                flurry.unlockedAbility = true;
                flurry.setArrowDamage(15);
                flurry.setNumArrows(5);
                flurry.setCooldown(8);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                flurry.setArrowDamage(20);
                flurry.setNumArrows(10);
                flurry.setCooldown(7);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                flurry.setArrowDamage(24);
                flurry.setNumArrows(16);
                flurry.setCooldown(6);
            }
        }
        else if (upgrade.itemName == "Sharpshooter")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.CrtBuff = 10;
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.CrtBuff = 20;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.CrtBuff = 30;
            }
        }

        //ROHAN
        if (upgrade.itemName == "Swift Snipping")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.SpdBuff = 10;
                upgrade.HasteBuff = 20;
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.SpdBuff = 15;
                upgrade.HasteBuff = 30;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.SpdBuff = 20;
                upgrade.HasteBuff = 40;
            }
        }
        else if (upgrade.itemName == "Double Trouble")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                doubleTrouble = GetComponent<Rohan_Skill_DoubleTrouble>();
                doubleTrouble.unlockedAbility = true;
                doubleTrouble.setProjectileDamage(30);
                doubleTrouble.setCooldown(8);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                doubleTrouble.setProjectileDamage(35);
                doubleTrouble.setCooldown(6);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                doubleTrouble.setProjectileDamage(40);
                doubleTrouble.setCooldown(4);
            }
        }

        //VAISHAK
        if (upgrade.itemName == "Spin")
        {
            SteelBallAttack steelBallAttack = GetComponent<SteelBallAttack>();
            if (upgrade.currentUpgradeLvl == 1)
            {
                steelBallAttack.SetSteelBallActive();
                steelBallAttack.SetDamage(30);
                steelBallAttack.SetDegreesTillHidden(360f);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                steelBallAttack.SetDamage(35);
                steelBallAttack.SetDegreesTillHidden(720f);
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
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.MaxHpBuff = 15;
                upgrade.PurBuff = 20;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.MaxHpBuff = 20;
                upgrade.PurBuff = 25;
            }
        }

        //WINFRED
        if (upgrade.itemName == "Scrambled")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                scrambled = gameObject.GetComponent<Winfred_Skill_Scrambled>();
                scrambled.unlockedAbility = true;
                scrambled.setDamage(40);
                scrambled.setCooldown(10);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                scrambled.setDamage(45);
                scrambled.setCooldown(8);
                scrambled.setExplosionScale(new Vector3(1.2f, 1.2f, 1f));
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                scrambled.setDamage(50);
                scrambled.setCooldown(6);
                scrambled.setExplosionScale(new Vector3(1.5f, 1.5f, 1f));
            }
        }
        else if (upgrade.itemName == "R U R' U'")
        {
            if (upgrade.currentUpgradeLvl == 1)
            {
                upgrade.SpdBuff = 10;
                upgrade.HasteBuff = 20;
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                upgrade.SpdBuff = 15;
                upgrade.HasteBuff = 30;
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                upgrade.SpdBuff = 20;
                upgrade.HasteBuff = 40;
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
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                decoy.setCooldown(9);
                decoy.setDuration(3);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                decoy.setCooldown(8);
                decoy.setDuration(4);
            }
        }
    }
}
