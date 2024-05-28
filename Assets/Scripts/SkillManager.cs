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

    [Header("Rohan")]
    private Rohan_Skill_DoubleTrouble doubleTrouble;

    [Header("Vaishak")]
    private Vaishak_Skill_Spin spin;

    [Header("Winfred")]
    private Winfred_Skill_Scrambled scrambled;

    private float enemyCheckCooldown = 10f;
    private float enemyCountRange = 5f;
    private float prevEnemyHeal;
    [SerializeField] private LayerMask enemyLayer;

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

    }
    
    public void Upgrade(UpgradeItem upgrade){
        //ARNAV
        if (upgrade.itemName == "Band for Band")
        {

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
                spearCircle.gameObject.transform.localScale = new Vector2(1.25f, 1.25f);
            }
            if (upgrade.currentUpgradeLvl == 3){
                spearCircle.setDamage(40);
                spearCircle.setCooldown(6);
                spearCircle.gameObject.transform.localScale = new Vector2(1.5f, 1.5f);
            }
        }
        else if (upgrade.itemName == "Time Crunch")
        {

        }
        else if (upgrade.itemName == "Debugging Hell")
        {

        }

        //JEMI
        if (upgrade.itemName == "I'll Be Back")
        {

        }
        else if (upgrade.itemName == "Fair and Balanced")
        {

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

        }
        else if (upgrade.itemName == "User Fees")
        {

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

        }

        //ROHAN
        if (upgrade.itemName == "Swift Snipping")
        {

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
            if (upgrade.currentUpgradeLvl == 1)
            {
                spin = gameObject.GetComponent<Vaishak_Skill_Spin>();
                spin.unlockedAbility = true;
                spin.setProjectileDamage(30);
                spin.setCooldown(6);
            }
            if (upgrade.currentUpgradeLvl == 2)
            {
                spin.setProjectileDamage(35);
                spin.setCooldown(5);
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                spin.setProjectileDamage(40);
                spin.setCooldown(4);
            }
        }
        else if (upgrade.itemName == "Big Hat")
        {

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
                //increase size?
            }
            if (upgrade.currentUpgradeLvl == 3)
            {
                scrambled.setDamage(50);
                scrambled.setCooldown(6);
                //increase size?
            }
        }
        else if (upgrade.itemName == "R U R' U'")
        {

        }
        else if (upgrade.itemName == "1x1")
        {

        }
    }
}
