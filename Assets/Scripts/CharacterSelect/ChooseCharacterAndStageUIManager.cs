using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChooseCharacterAndStageUIManager : MonoBehaviour
{
    [Header("Choose Character (Make sure images are listed in alphabetical order :p)")]
    [SerializeField] List<Sprite> characterImages = new List<Sprite>();
    private List<string> characterNames = new List<string>();
    [SerializeField] private List<GameObject> charaPrefabs = new List<GameObject>();
    private int curCharaIndex = 3; //jolie
    [SerializeField] Image charaImage;
    [SerializeField] TMP_Text charaName;

    [Header("Choose Stage (only one stage for now)")]
    private int curStageIndex = 0;

    public void Start()
    {
        characterNames.Add("Arnav");
        characterNames.Add("Faye");
        characterNames.Add("Jemian");
        characterNames.Add("Jolie");
        characterNames.Add("Laurier");
        characterNames.Add("Lydia");
        characterNames.Add("Rohan");
        characterNames.Add("Vaishak");
        characterNames.Add("Winfred");
    }

    public void Gacha()
    {
        SceneManager.LoadScene(2); //scene name: Gacha
    }
    public void chooseCharacterLeft()
    {
        curCharaIndex--;
        if (curCharaIndex < 0) curCharaIndex = 8;
        charaImage.sprite = characterImages[curCharaIndex];
        charaName.text = characterNames[curCharaIndex];
        PlayerPrefs.SetString("SelectedCharacter", charaName.text + " (P)");
    }
    public void chooseCharacterRight()
    {
        curCharaIndex++;
        if (curCharaIndex >= 9) curCharaIndex = 0;
        charaImage.sprite = characterImages[curCharaIndex]; //probably can make method for this but whatever
        charaName.text = characterNames[curCharaIndex];
        PlayerPrefs.SetString("SelectedCharacter", charaName.text + " (P)");
    }
    public void chooseStageLeft()
    {
        curStageIndex--;
    }
    public void chooseStageRight()
    {
        curStageIndex++;
    }
    public void PlayStage()
    {
        //tell the next scene to get the chosen stage and character
        SceneManager.LoadScene(3); //scene name: Stage 1
    }
    public void goBackToStartMenu()
    {
        SceneManager.LoadScene(0); //scene name: StartScreen
    }
}
