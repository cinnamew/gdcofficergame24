using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelXPManager : MonoBehaviour
{
    public int xp = 0;
    private int xpForLevelUp = 79;
    private int level = 1;
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text levelText;
    void Start()
    {
        UpdateSlider();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("xp")) 
        { 
            System.Random r = new System.Random();
            switch (other.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "XP_0":
                    updateXP(r.Next(1, 11)); // (inclusive, exclusive)
                    break;   
                case "XP_1":
                    updateXP(r.Next(11, 20)); // (inclusive, exclusive)
                    break;
                case "XP_2":
                    updateXP(r.Next(20, 50)); // (inclusive, exclusive)
                    break;
                case "XP_3":
                    updateXP(r.Next(50, 100)); // (inclusive, exclusive)
                    break;

            }
            Destroy(other.gameObject);
        }
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
    }
    private void UpdateSlider(){
        if (slider != null)
        {
            slider.maxValue = xpForLevelUp;
            slider.value = xp;
        }
    }
}
