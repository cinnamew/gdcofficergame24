using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Sorry for the ungodly amount of serialized field rohan :(
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject[] icons;
    [SerializeField] TMP_Text[] itemNames;
    [SerializeField] TMP_Text[] itemDescriptions;
    [SerializeField] List<UpgradeItem> generalUpgrades;
    private List<UpgradeItem> exclusiveUpgrades;
    private const int upgradesPerLevel = 4;
    private GameObject player;
    private List<UpgradeItem> upgradeItems; 

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("apsoidjfasijdfjaspofjasoipdfj" + player);
        exclusiveUpgrades = player.GetComponent<LevelXPManager>().GetExclusiveUpgrades(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelMenu(){
        levelPanel.SetActive(true);
        upgradeItems = PickRandomUpgrades();
        DisplayUI();
    }

    public void SelectOption(int option){
        player.GetComponent<UpgradeInventory>().AddItem(upgradeItems[option]);
        levelPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private List<UpgradeItem> PickRandomUpgrades(){
        List<UpgradeItem> randomItems = new List<UpgradeItem>();
        for (int i = 0; i < upgradesPerLevel; i++){
            int randomIndex = Random.Range(0, exclusiveUpgrades.Count + generalUpgrades.Count);
            if (randomIndex < exclusiveUpgrades.Count){
                randomItems.Add(exclusiveUpgrades[randomIndex]);
            } else {
                randomItems.Add(generalUpgrades[randomIndex - exclusiveUpgrades.Count]);
            }
            //if (player.GetComponent<UpgradeInventory>().HasItem(randomItems))
        }
        return randomItems;
    }

    private void DisplayUI(){
        for (int i = 0; i < icons.Length; i++){
            UpgradeItem currentItem = upgradeItems[i];
            icons[i].GetComponent<Image>().sprite = currentItem.icon;
            if (currentItem.currentUpgradeLvl > 1){
                itemNames[i].SetText(currentItem.itemName);
            } else {
                itemNames[i].SetText(currentItem.itemName + " LVL" + currentItem.currentUpgradeLvl);
            }
            itemDescriptions[i].SetText(currentItem.description);
        }
    }


}
