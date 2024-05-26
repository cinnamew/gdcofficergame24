using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    
    private int coins;
    private List<string> charactersUnlocked = new List<string>();
    
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

    public void unlockCharacter(string s) {
        if(charactersUnlocked.Contains(s)) return;
        charactersUnlocked.Add(s);
    }
    public void goToPreviousMenu()
    {
        SceneManager.LoadScene(1); //scene name: ChooseCharacterAndStage
    }
}
