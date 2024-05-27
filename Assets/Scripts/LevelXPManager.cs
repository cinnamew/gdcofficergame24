using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class LevelXPManager : MonoBehaviour
{
    public int xp = 0;
    private int xpForLevelUp = 79;
    private int level = 1;
    private Slider slider;
    private TMP_Text levelText;
    [SerializeField] List<UpgradeItem> exclusiveUpgrades;
    [SerializeField] PickUpManager pickUpManager;
    private StatsManager statsManager;
    private StageUIManager uiManager;
    void Start()
    {
        UpdateSlider();
        uiManager = GameObject.Find("Canvas").GetComponent<StageUIManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        slider = uiManager.GetXPSlider();
        levelText = uiManager.GetLevelText();
    }

    public List<UpgradeItem> GetExclusiveUpgrades(){
        return exclusiveUpgrades;
    }

    private void Update(){
        pickUpManager.SetRadius(0.75f*statsManager.Pur/100f);
    }
    
    public void XPCollected(Collider2D other)
    {
        System.Random r = new System.Random();
        switch (other.gameObject.GetComponent<SpriteRenderer>().sprite.name)
        {
            case "xp_with_borders_0":
                updateXP(r.Next(1, 11)); // (inclusive, exclusive)
                break;   
            case "xp_with_borders_1":
                updateXP(r.Next(11, 20)); // (inclusive, exclusive)
                gameObject.GetComponent<Health>().HealHealth(10);
                break;
            case "xp_with_borders_2":
                updateXP(r.Next(20, 50)); // (inclusive, exclusive)
                gameObject.GetComponent<Health>().HealHealth(15);
                break;
            case "xp_with_borders_3":
                updateXP(r.Next(50, 100)); // (inclusive, exclusive)
                gameObject.GetComponent<Health>().HealHealth(20);
                break;
        }
        Destroy(other.gameObject);
    }
    
    public void updateXP(int xpGained) 
    {
        xp += xpGained;
        if (xp >= xpForLevelUp)
        {
            LevelUp();
        }
        UpdateSlider();
    }

    private void LevelUp(){
        xp -= xpForLevelUp;
        xpForLevelUp = Convert.ToInt32(Math.Pow(4*level+4, 2.1)) - Convert.ToInt32(Math.Pow(4*level, 2.1));
        level++;
        if (levelText != null){
            levelText.SetText("Level " + level);
        }
        uiManager.ShowLevelMenu();
        Time.timeScale = 0;
    }
    private void UpdateSlider(){
        if (slider != null)
        {
            slider.maxValue = xpForLevelUp;
            slider.value = xp;
        }
    }
}
