using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void Upgrade(UpgradeItem upgrade){
        if (upgrade.itemName == "Social Anxiety"){ //duplicate for other skills
            if (upgrade.currentUpgradeLvl == 1){
                //stuff here
            }
            if (upgrade.currentUpgradeLvl == 2){
                //stuff here
            }
            if (upgrade.currentUpgradeLvl == 3){
                //stuff here
            }
        }
    }
}
