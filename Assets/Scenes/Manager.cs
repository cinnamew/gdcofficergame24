using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : Singleton<Manager>
{
    [SerializeField] private int coins;
    [SerializeField] private Dictionary<string, int> charactersUnlocked = new Dictionary<string, int>(); //everything is lowercase
    //private List<string> charactersUnlocked = new List<string>();

    [SerializeField] List<string> charaNames = new List<string> {"arnav", "jolie", "vaishak", "lydia", "faye", "winfred", "rohan", "laurier", "jemi"};
    
    [SerializeField] bool resetGachaPulls = false;

    //these MUST be in the same order!!
    [SerializeField] List<PlayerStats> playerStats;
    [SerializeField] List<PlayerStats> basePlayerStats;


    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
        if(!PlayerPrefs.HasKey("coins")) coins = 100;
        else {
            coins = PlayerPrefs.GetInt("coins");
        }
        if(resetGachaPulls) ResetPulls();
        for(int i = 0; i < charaNames.Count; i++) {
            string temp = charaNames[i].ToLower();
            if(PlayerPrefs.HasKey(temp)) {
                charactersUnlocked[charaNames[i]] = PlayerPrefs.GetInt(temp);
            }
        }
        RefreshPlayerStats();
    }

    public void ResetPulls() {
        for(int i = 0; i < charaNames.Count; i++) {
            PlayerPrefs.SetInt(charaNames[i], 0);
        }
    }

    public PlayerStats GetPlayerStats(string name) {
         string lName = name.ToLower();
         for(int i = 0; i < playerStats.Count; i++) {
             if(lName == playerStats[i].name.ToLower()) return playerStats[i];
         }
         return null;
     }

    public void RefreshPlayerStats() {
        for(int i = 0; i < playerStats.Count; i++) {
            int num = hasCharacter(playerStats[i].name);
            if(num == 0) continue;
            playerStats[i].MaxHp = basePlayerStats[i].MaxHp;
            playerStats[i].Atk = basePlayerStats[i].Atk;
            playerStats[i].Spd = basePlayerStats[i].Spd;
            playerStats[i].Crt = basePlayerStats[i].Crt;
            playerStats[i].Pur = basePlayerStats[i].Pur;

            float lastHPBoost = 0.5f*playerStats[i].MaxHp;
            float lastAtkBoost = 0.5f*playerStats[i].Atk;
            float lastSpdBoost = 0.5f*playerStats[i].Spd;
            float lastCrtBoost = 0.5f*playerStats[i].Crt;
            for(int j = 1; j < num; j++) {
                playerStats[i].MaxHp += (int)lastHPBoost;
                playerStats[i].Atk += lastAtkBoost;
                playerStats[i].Spd += lastSpdBoost;
                playerStats[i].Crt += lastCrtBoost; 

                lastHPBoost *= 0.5f;
                lastAtkBoost *= 0.5f;
                lastSpdBoost *= 0.5f;
                lastCrtBoost *= 0.5f;
            }
         }
     }

     public PlayerStats GetBasePlayerStats(string name) {
        string lName = name.ToLower();
         for(int i = 0; i < basePlayerStats.Count; i++) {
             if(lName == basePlayerStats[i].name.ToLower()) return basePlayerStats[i];
         }
         return null;
     }

    public int getCoins() {
        return coins;
    }

    public void addToCoins(int a) {
        coins += a;
        PlayerPrefs.SetInt("coins", coins);
        print("coins: " + coins);
        PlayerPrefs.Save();
    }

    public bool decreaseNumCoins(int a) {
        if (coins - a >= 0){
            coins -= a;
            PlayerPrefs.SetInt("coins", coins);
            print("coins: " + coins);
            return true;
        }
        return false;
    }
    public void addCharacter(string s) {
        s = s.ToLower();
        if(charactersUnlocked.ContainsKey(s)) {
            charactersUnlocked[s] = charactersUnlocked[s] + 1;
            PlayerPrefs.SetInt(s, charactersUnlocked[s]);
            RefreshPlayerStats();
        }
        else {
            charactersUnlocked[s] = 1;
            PlayerPrefs.SetInt(s, 1);
        }
    }

    public int hasCharacter(string s) {
        s = s.ToLower();
        if(PlayerPrefs.HasKey(s)) return PlayerPrefs.GetInt(s);
        return 0;
    }

    public void goToPreviousMenu()
    {
        SceneManager.LoadScene(1); //scene name: ChooseCharacterAndStage
    }
}
