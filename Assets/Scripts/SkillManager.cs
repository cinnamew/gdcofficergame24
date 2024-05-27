using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Arnav")]
    private Arnav_Skill_Lightning lightning;

    [Header("Faye")]
    private Faye_Skill_SocialAnxiety spearCircle;

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

        }

        //JOLIE
        if (upgrade.itemName == "Heart Attack!")
        {

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
