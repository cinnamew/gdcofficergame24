using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    
    public int MaxHp;
    public float Atk; //the attack damage multiplier
    public float Spd;//player speed. SPD
    public float Crt; //percentage chance for critical hit. CRT
    public float Pur; //short for pickup radius
    public float Haste; //attackTime = round( baseAttackTime/(1 + haste%/100% ) )
    // Start is called before the first frame update
    private void Awake(){
        UpdateAttributes();
    }

    public void SetPlayerStats(PlayerStats newStats){
        playerStats = newStats;
        UpdateAttributes();
    }

    private void UpdateAttributes(){
        MaxHp = playerStats.MaxHp;
        Atk = playerStats.Atk;
        Spd = playerStats.Spd;
        Crt = playerStats.Crt;
        Pur = playerStats.Pur;
        Haste = playerStats.Haste;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
