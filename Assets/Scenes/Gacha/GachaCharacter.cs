using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaCharacter : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] string name;

    [SerializeField] string bio;
    //[SerializeField] int hp;
    //[SerializeField] float attack;
    //[SerializeField] float crit;
    //[SerializeField] int spd;
    private PlayerStats playerStats;
    
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        //playerStats = Manager.Obj.GetPlayerStats(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getStats() {
        if(playerStats == null) playerStats = Manager.Obj.GetPlayerStats(name);
        string temp = "";
        if(Manager.Obj.hasCharacter(name) == 0) {
            temp = "HP: ?\nATK: ?\nCRT: ?\nSPD: ?";
        }else {
            TempCharacter yum = nextLevel(Manager.Obj.hasCharacter(name) + 1);
            temp = "HP: " + playerStats.MaxHp + " -> " + yum.MaxHp + "\nATK: " + playerStats.Atk + " -> " + yum.Atk + "\nCRT: " + playerStats.Crt + " -> " + yum.Crt + "\nSPD: " + playerStats.Spd + " -> " + yum.Spd;
        }
        return temp;
    }

    public TempCharacter nextLevel(int lvl) {
        TempCharacter temp = new TempCharacter();
        temp.MaxHp = playerStats.MaxHp;
        temp.Atk = playerStats.Atk;
        temp.Spd = playerStats.Spd;
        temp.Crt = playerStats.Crt;
        float lastHPBoost = 0.5f*temp.MaxHp;
        float lastAtkBoost = 0.5f*temp.Atk;
        float lastSpdBoost = 0.5f*temp.Spd;
        float lastCrtBoost = 0.5f*temp.Crt;
        for(int j = 0; j < lvl; j++) {
            temp.MaxHp += (int)lastHPBoost;
            temp.Atk += lastAtkBoost;
            temp.Spd += lastSpdBoost;
            temp.Crt += lastCrtBoost; 

            lastHPBoost *= 0.5f;
            lastAtkBoost *= 0.5f;
            lastSpdBoost *= 0.5f;
            lastCrtBoost *= 0.5f;

        }
        return temp;
    }

    public string getBio() {
        return bio;
    }

    public string getName() {
        return name;
    }
}

[System.Serializable]
public class TempCharacter {
    public string name;
    public int MaxHp;
    public float Atk; //the attack damage multiplier
    public float Spd; //player speed. SPD
    public float Crt; //percentage chance for critical hit. CRT
}
