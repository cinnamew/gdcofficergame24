using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    //Sorry for the ungodly amount of serialized field rohan :(
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject[] icons;
    [SerializeField] TMP_Text[] itemNames;
    [SerializeField] TMP_Text[] itemDescriptions;
    [SerializeField] List<UpgradeItem> generalUpgrades;
    private List<UpgradeItem> exclusiveUpgrades;
    private List<UpgradeItem> upgrades = new List<UpgradeItem>();
    private const int upgradesPerLevel = 4;
    private GameObject player;
    public List<UpgradeItem> temp;
    private List<UpgradeItem> upgradeItems;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] GameObject pauseButton;
    private StatsManager statsManager;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        Debug.Log("apsoidjfasijdfjaspofjasoipdfj" + player);
        exclusiveUpgrades = player.GetComponent<LevelXPManager>().GetExclusiveUpgrades(); 
        upgrades.AddRange(exclusiveUpgrades);
        upgrades.AddRange(generalUpgrades);
        for (int i = 0; i < upgrades.Count; i++){
            upgrades[i].Reset();
        }
    }

    public Slider GetHealthSlider(){
        return healthSlider;
    }

    public Slider GetXPSlider(){
        return xpSlider;
    }
    public TMP_Text GetLevelText(){
        return levelText;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelMenu(){
        levelPanel.SetActive(true);
        pauseButton.SetActive(false);
        upgradeItems = PickRandomUpgrades();
        DisplayUI();
    }

    public void SelectOption(int option){
        player.GetComponent<UpgradeInventory>().AddItem(upgradeItems[option]);
        if (upgradeItems[option].maxUpgradeLvl != -1 && upgradeItems[option].currentUpgradeLvl >= upgradeItems[option].maxUpgradeLvl){
            upgrades.Remove(upgradeItems[option]);
        } else {
            upgradeItems[option].Upgrade();
        }
        if (upgradeItems[option].IsWeapon){
            player.GetComponent<WeaponManager>().Upgrade(upgradeItems[option]);
        } else if (upgradeItems[option].IsSkill){
            player.GetComponent<SkillManager>().Upgrade(upgradeItems[option]);
        } else if (upgradeItems[option].IsStat){
            upgradeItems[option].ApplyBuffs(statsManager);
        }
        levelPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    private List<UpgradeItem> PickRandomUpgrades(){
        temp = new List<UpgradeItem>();
        temp.AddRange(upgrades);
        List<UpgradeItem> randomItems = new List<UpgradeItem>();
        for (int i = 0; i < upgradesPerLevel; i++){
            int randomIndex = Random.Range(0, temp.Count);
            randomItems.Add(temp[randomIndex]);
            temp.RemoveAt(randomIndex);
            //if (player.GetComponent<UpgradeInventory>().HasItem(randomItems))
        }
        return randomItems;
    }

    private void DisplayUI(){
        for (int i = 0; i < icons.Length; i++){
            UpgradeItem currentItem = upgradeItems[i];
            icons[i].GetComponent<Image>().sprite = currentItem.icon;
            if (currentItem.currentUpgradeLvl > 1){
                itemNames[i].SetText(currentItem.itemName + " LVL" + currentItem.currentUpgradeLvl);
            } else {
                itemNames[i].SetText(currentItem.itemName);
            }
            itemDescriptions[i].SetText(currentItem.description);
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void goToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); //scene name: StartScreen
    }
    public void replayStage()
    {
        Time.timeScale = 1; //assuming we pause when stage complete
        SceneManager.LoadScene(3); //this scene, Stage 1
    }
    public void goToCharacterSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
