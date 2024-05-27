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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
        if(!PlayerPrefs.HasKey("coins")) coins = 20;
        else {
            coins = PlayerPrefs.GetInt("coins");
        }
        for(int i = 0; i < charaNames.Count; i++) {
            string temp = charaNames[i].ToLower();
            if(PlayerPrefs.HasKey(temp)) {
                charactersUnlocked[charaNames[i]] = PlayerPrefs.GetInt(temp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCoins() {
        return coins;
    }

    public void addToCoins(int a) {
        coins += a;
        PlayerPrefs.SetInt("coins", coins);
        print("coins: " + coins);
    }

    public void addCharacter(string s) {
        s = s.ToLower();
        if(charactersUnlocked.ContainsKey(s)) {
            charactersUnlocked[s] = charactersUnlocked[s] + 1;
            PlayerPrefs.SetInt(s, charactersUnlocked[s]);
        }
        else {
            charactersUnlocked[s] = 1;
            PlayerPrefs.SetInt(s, 1);
        }
    }

    public int hasCharacter(string s) {
        s = s.ToLower();
        if(charactersUnlocked.ContainsKey(s)) return charactersUnlocked[s];
        return 0;
    }

    public void goToPreviousMenu()
    {
        SceneManager.LoadScene(1); //scene name: ChooseCharacterAndStage
    }
}
