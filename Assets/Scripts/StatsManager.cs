using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    
    public int MaxHp {get{return playerStats.MaxHp;} set{playerStats.MaxHp = value;}}
    public float Atk {get{return playerStats.Atk;} set{playerStats.Atk = value;}} //the attack damage multiplier
    public float Spd {get{return playerStats.Spd;} set{playerStats.Spd = value;}} //player speed. SPD
    public float Crt {get{return playerStats.Crt;} set{playerStats.Crt = value;}} //percentage chance for critical hit. CRT
    public float Pur {get{return playerStats.Pur;} set{playerStats.Pur = value;}} //short for pickup radius
    public float Haste {get{return playerStats.Haste;} set{playerStats.Haste = value;}} //attackTime = round( baseAttackTime/(1 + haste%/100% ) )
    // Start is called before the first frame update

    public void SetPlayerStats(PlayerStats newStats){
        playerStats = newStats;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
