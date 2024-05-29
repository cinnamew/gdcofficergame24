using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageUIManager : Singleton<StageUIManager>
{
    //Sorry for the ungodly amount of serialized field rohan :(
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject[] icons;
    [SerializeField] TMP_Text[] itemNames;
    [SerializeField] TMP_Text[] itemDescriptions;
    [SerializeField] List<UpgradeItem> generalUpgrades;
    [SerializeField] bool includeGeneralUpgrades = true;
    private List<UpgradeItem> exclusiveUpgrades;
    private List<UpgradeItem> upgrades = new List<UpgradeItem>();
    private const int upgradesPerLevel = 4;
    private GameObject player;
    public List<UpgradeItem> temp;
    private List<UpgradeItem> temp2;
    private List<UpgradeItem> temp3;
    private List<UpgradeItem> upgradeItems;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject stageCompleteMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] GameObject pauseButton;
    [SerializeField] TMP_Text coinsText;
    private StatsManager statsManager;
    private bool gameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        Debug.Log("apsoidjfasijdfjaspofjasoipdfj" + player);
        exclusiveUpgrades = player.GetComponent<LevelXPManager>().GetExclusiveUpgrades(); 
        upgrades.AddRange(exclusiveUpgrades);
        if (includeGeneralUpgrades){
            upgrades.AddRange(generalUpgrades);
        }
        for (int i = 0; i < upgrades.Count; i++){
            upgrades[i].Reset();
        }
        coinsText.text = Manager.Obj.getCoins() + "";
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
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOver) {
            if(Time.timeScale != 0) Pause();
            else Resume();
        }
    }

    public void EndOfLevelReached(){
        stageCompleteMenu.SetActive(true);
        gameOver = true;
        Time.timeScale = 0;
    }

    public void Died(){
        deathMenu.SetActive(true);
        gameOver = true;
        Time.timeScale = 0;
    }

    public void ShowLevelMenu(){
        levelPanel.SetActive(true);
        pauseButton.SetActive(false);
        upgradeItems = PickRandomUpgrades();
        DisplayUI();
    }

    public void SelectOption(int option){
        player.GetComponent<UpgradeInventory>().AddItem(upgradeItems[option]);
        if (upgradeItems[option].IsWeapon){
            Debug.Log("PLEASEE FOR THE LLOVE OF ALL THINGS WORKKKK");
            player.GetComponent<WeaponManager>().Upgrade(upgradeItems[option]);
        } else if (upgradeItems[option].IsSkill){
            player.GetComponent<SkillManager>().Upgrade(upgradeItems[option]);
        } else if (upgradeItems[option].IsStat){
            upgradeItems[option].ApplyBuffs(statsManager);
        } else {
            player.GetComponent<GeneralUpgradesManager>().Upgrade(upgradeItems[option]);
        }
        if (upgradeItems[option].maxUpgradeLvl != -1 && upgradeItems[option].currentUpgradeLvl >= upgradeItems[option].maxUpgradeLvl){
            if (upgradeItems[option].IsSkill || upgradeItems[option].IsWeapon){
                exclusiveUpgrades.Remove(upgradeItems[option]);
            } else {
                generalUpgrades.Remove(upgradeItems[option]);
            }
            upgrades.Remove(upgradeItems[option]);
        } else {
            upgradeItems[option].Upgrade();
        }
        updateDescription(upgradeItems[option]);
        levelPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    private void updateDescription(UpgradeItem upgradeItem){
        if (upgradeItem.itemName == "BL Book"){
            if (upgradeItem.currentUpgradeLvl > 1){
                upgradeItem.description = "Add 1 additional book.";
            }
        }
        if (upgradeItem.itemName == "Spider Cooking"){
            if (upgradeItem.currentUpgradeLvl == 2){
                upgradeItem.description = "Increase area by 30%.";
            }
            if (upgradeItem.currentUpgradeLvl == 3){
                upgradeItem.description = "Increase damage by 30%.";
            }
            if (upgradeItem.currentUpgradeLvl == 4){
                upgradeItem.description = "Increase area by 50%.";
            }
            if (upgradeItem.currentUpgradeLvl == 5){
                upgradeItem.description = "Increase frequency of hits by 20%.";
            }
            if (upgradeItem.currentUpgradeLvl == 6){
                upgradeItem.description = "Increase damage by 60%";
            }
            if (upgradeItem.currentUpgradeLvl == 7){
                upgradeItem.description = "Add small knockback on hit.";
            }
        }
        if (upgradeItem.itemName == "BL Book"){
            if (upgradeItem.currentUpgradeLvl > 1){
                upgradeItem.description = "Add 1 additional book.";
            }
        }
        if (upgradeItem.itemName == "Social Anxiety"){
            if (upgradeItem.currentUpgradeLvl > 1){
                upgradeItem.description = "Increase damage by 5, increase swing size by 20% and decrease cooldown by 10%";
            }
        }
        if (upgradeItem.itemName == "Debugging Hell"){
            if (upgradeItem.currentUpgradeLvl > 1){
                upgradeItem.description = "Increase heal amount by 2%";
            }
        }
        if (upgradeItem.itemName == "Berserk"){
            if (upgradeItem.currentUpgradeLvl == 2){
                upgradeItem.description = "Increase damage by 3, decrease cooldown by 20%, and increase number of projectiles by 6.";
            }
            if (upgradeItem.currentUpgradeLvl == 3){
                upgradeItem.description = "Increase damage by 2, decrease cooldown by 20%, and increase number of projectiles by 6.";
            }
        }
        if (upgradeItem.itemName == "Enemies to Lovers"){
            if (upgradeItem.currentUpgradeLvl > 1){
                upgradeItem.description = "Increase count range by 5 meters, and decrease cooldown by 20%";
            }
        }
        if (upgradeItem.itemName == "Empathy"){
            if (upgradeItem.currentUpgradeLvl > 1){
                upgradeItem.description = "Double enemy damage amount, decrease damage cooldown by 30%, and increase lifesteal by 3%";
            }
        }
    }

    private List<UpgradeItem> PickRandomUpgrades(){
        temp = new List<UpgradeItem>();
        temp2 = new List<UpgradeItem>();
        temp3 = new List<UpgradeItem>();
        temp.AddRange(upgrades);
        temp2.AddRange(exclusiveUpgrades);
        temp3.AddRange(generalUpgrades);        
        List<UpgradeItem> randomItems = new List<UpgradeItem>();
        for (int i = 0; i < upgradesPerLevel; i++){
            bool isExclusive = Random.Range(0, 2) < 1;
            if (isExclusive && temp2.Count > 0){
                int randomIndex = Random.Range(0, temp2.Count);
                randomItems.Add(temp2[randomIndex]);
                temp2.RemoveAt(randomIndex);
            } else {
                int randomIndex = Random.Range(0, temp3.Count);
                randomItems.Add(temp3[randomIndex]);
                temp3.RemoveAt(randomIndex);
            }
            //if (player.GetComponent<UpgradeInventory>().HasItem(randomItems))
        }
        return randomItems;
    }

    private void DisplayUI(){
        for (int i = 0; i < icons.Length; i++){
            UpgradeItem currentItem = upgradeItems[i];
            icons[i].GetComponent<Image>().sprite = currentItem.icon;
            if (currentItem.currentUpgradeLvl == currentItem.maxUpgradeLvl){
                itemNames[i].SetText(currentItem.itemName + " MAX LVL");
            } else if (currentItem.currentUpgradeLvl > 1){
                itemNames[i].SetText(currentItem.itemName + " LVL" + currentItem.currentUpgradeLvl);
            } else {
                itemNames[i].SetText(currentItem.itemName);
            }
            itemDescriptions[i].SetText(currentItem.description);
        }
    }
    public void Pause()
    {
        if (!gameOver){
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void Resume()
    {
        if (gameOver){
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
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

    public void UpdateCoinsText() {
        coinsText.text = Manager.Obj.getCoins() + "";
    }
}
