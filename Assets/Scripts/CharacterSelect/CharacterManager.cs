using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] charaPrefabs;
    public Vector3 spawnPos;
    [SerializeField] private ProCamera2D cam;
    
    // Start is called before the first frame update
    void Awake()
    {
        //check if player prefs string is not null or empty
        //convert playerprefs into chara prefab
        //check if chara is null, error if there is no prefab assosciating the string
        //if not null instantiate the character at the spawn point
        string charaString = PlayerPrefs.GetString("SelectedCharacter");
        Debug.Log("PLAYER NAME: " + charaString);
        if (!string.IsNullOrEmpty(charaString)) 
        {
            GameObject charaObject = getCharaByName(charaString);
            if (charaObject != null) 
            {
                GameObject playerInScene = Instantiate(charaObject, spawnPos, Quaternion.identity);
                cam.AddCameraTarget(playerInScene.transform);
            }
        }

    }

    GameObject getCharaByName(string nameAsString) 
    {
        foreach (GameObject g in charaPrefabs) 
        {
            if (nameAsString == g.name)
                return g;
        }
        return null;
    }
}
