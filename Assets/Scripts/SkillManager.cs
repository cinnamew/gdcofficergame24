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

    [Header("Lydia")]
    private Lydia_Skill_Flurry flurry;

    [Header("Rohan")]
    private Rohan_Skill_DoubleTrouble doubleTrouble;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
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

        }
        else if (upgrade.itemName == "Enemies to Lovers")
        {

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

        }
        else if (upgrade.itemName == "Big Hat")
        {

        }

        //WINFRED
        if (upgrade.itemName == "Scrambled")
        {

        }
        else if (upgrade.itemName == "R U R' U'")
        {

        }
        else if (upgrade.itemName == "1x1")
        {

        }
    }
}
