using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] charaPrefabs;
    public PlayerStats[] playerStats;
    public Vector3 spawnPos;
    [SerializeField] private ProCamera2D cam;
    private StatsManager statsManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        //check if player prefs string is not null or empty
        //convert playerprefs into chara prefab
        //check if chara is null, error if there is no prefab assosciating the string
        //if not null instantiate the character at the spawn point
        string charaString = PlayerPrefs.GetString("SelectedCharacter");
        Debug.Log("PLAYER NAME: " + charaString);
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        if (!string.IsNullOrEmpty(charaString)) 
        {
            int charaObjectIndex = getCharaByName(charaString);
            if (charaObjectIndex > -1) 
            {
                GameObject charaObject = charaPrefabs[charaObjectIndex];
                statsManager.SetPlayerStats(playerStats[charaObjectIndex]);
                GameObject playerInScene = Instantiate(charaObject, spawnPos, Quaternion.identity);
                cam.AddCameraTarget(playerInScene.transform);
            }
        }

    }

    int getCharaByName(string nameAsString) 
    {
        for (int i = 0; i < charaPrefabs.Length; i++){
            if (nameAsString == charaPrefabs[i].name){
                return i;
            }
        }
        return -1;
    }
}
