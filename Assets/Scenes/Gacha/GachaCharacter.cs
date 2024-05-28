using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    private PlayerStats basePlayerStats;
    
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
            //Manager.Obj.RefreshPlayerStats();
            TempCharacter yum = nextLevel(Manager.Obj.hasCharacter(name) + 1);
            temp = "HP: " + Math.Round((Decimal)playerStats.MaxHp, 2) + " -> " + Math.Round((Decimal)yum.MaxHp, 2) + "\nATK: " + Math.Round((Decimal)playerStats.Atk, 2) + " -> " + Math.Round((Decimal)yum.Atk, 2) + "\nCRT: " + Math.Round(playerStats.Crt, 2) + " -> " + Math.Round((Decimal)yum.Crt, 2) + "\nSPD: " + Math.Round((Decimal)playerStats.Spd, 2) + " -> " + Math.Round((Decimal)yum.Spd, 2);
        }
        return temp;
    }

    public TempCharacter nextLevel(int lvl) {
        if(basePlayerStats == null) basePlayerStats = Manager.Obj.GetBasePlayerStats(name);
        TempCharacter temp = new TempCharacter();
        temp.MaxHp = basePlayerStats.MaxHp;
        temp.Atk = basePlayerStats.Atk;
        temp.Spd = basePlayerStats.Spd;
        temp.Crt = basePlayerStats.Crt;
        float lastHPBoost = 0.5f*basePlayerStats.MaxHp;
        float lastAtkBoost = 0.5f*basePlayerStats.Atk;
        float lastSpdBoost = 0.5f*basePlayerStats.Spd;
        float lastCrtBoost = 0.5f*basePlayerStats.Crt;
        for(int j = 1; j < lvl; j++) {
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
