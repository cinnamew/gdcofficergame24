using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    
    [SerializeField] private int coins;
    [SerializeField] private Dictionary<string, int> charactersUnlocked = new Dictionary<string, int>(); //everything is lowercase
    //private List<string> charactersUnlocked = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {

    }


    void Awake() {
        GameObject[] e = GameObject.FindGameObjectsWithTag("manager");
        if(e.Length > 1) {
            Destroy(e[0]);
        }else {
            if(!PlayerPrefs.HasKey("coins")) coins = 20;
            else {
                coins = PlayerPrefs.GetInt("coins");
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
    }

    public void addCharacter(string s) {
        s = s.ToLower();
        if(charactersUnlocked.ContainsKey(s)) {
            charactersUnlocked[s] = charactersUnlocked[s] + 1;
        }
        else charactersUnlocked[s] = 1;
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
