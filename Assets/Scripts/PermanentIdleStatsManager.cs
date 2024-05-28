using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentIdleStatsManager : Singleton<PermanentIdleStatsManager>
{
    [SerializeField] List<PlayerStats> playerStats;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     DontDestroyOnLoad(this);
    //     Reset();
    // }

    // public PlayerStats GetPlayerStats(string name) {
    //     lName = name.ToLower();
    //     for(int i = 0; i < playerStats.Count; i++) {
    //         if(lName == playerStats[i].toLower()) return playerStats[i];
    //     }
    //     return null;
    // }

    // public void Reset() {
    //     for(int i = 0; i < playerStats.Count; i++) {
    //         if(Manager.Obj.hasCharacter(playerStats[i].name) == 0) continue;
            
    //     }
    // }
}
