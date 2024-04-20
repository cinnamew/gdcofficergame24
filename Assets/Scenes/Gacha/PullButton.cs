using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullButton : MonoBehaviour
{
    [SerializeField] private List<Character> allCharas;
    //private List<Character> SRs = new List<Character>();
    //private List<Character> SSRs = new List<Character>();
    //private List<Character> URs = new List<Character>();

    private Manager manager;


    private int[] pullWeights = {50, 35, 15};
    private Button button;
    //public int rand;
    public GameObject pulledChara;

    [SerializeField] GameObject thingToHide;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager").gameObject.GetComponent<Manager>();
        //button = GetComponent<Button>();
        //button.onClick.AddListener(Pull);

        //foreach(Character c in allCharas) {
        //    //change to switch case if too slow
        //    if(c.rarity == "SR") SRs.Add(c);
        //    else if(c.rarity == "SSR") SSRs.Add(c);
        //    else URs.Add(c);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pull() {
        int coins = manager.getCoins();
        //print("hello pulling");

        //print(coins);

        //if (coins < 20) {
        //    print("not enough coins!");
        //    return;
        //}
        
        if(thingToHide != null) thingToHide.SetActive(false);

            //pulledChara.GetComponent<WaitForClick>().HideObjects();

            //rand = Random.Range(0, 100);

            //int rarity = 0; //0 = SR, 1 = SSR, 2 = UR

            //for (int i = 0; i < pullWeights.Length; i++)
            //{
            //    int weight = pullWeights[i];
            //    if (rand <= weight)
            //    {
            //        rarity = i;
            //        break;
            //    }
            //    rand -= weight;
            //}

            Character hot = allCharas[Random.Range(0,allCharas.Count)];  //the chara u pulled
            //int rand2 = Random.Range(0, SRs.Count);
            //switch (rarity)
            //{
            //    case 0:
            //        hot = SRs[rand2];
            //        break;
            //    case 1:
            //        rand2 = Random.Range(0, SSRs.Count);
            //        hot = SSRs[rand2];
            //        break;
            //    default:
            //        rand2 = Random.Range(0, URs.Count);
            //        hot = URs[rand2];
            //        break;
            //}

            pulledChara.GetComponent<SpriteRenderer>().sprite = hot.sprite;
            pulledChara.SetActive(true);

            print("u got the lovely " + hot.name);

            pulledChara.GetComponent<PulledChara>().WaitForMouseClick();

            GameObject.FindGameObjectWithTag("manager").gameObject.GetComponent<Manager>().addToCoins(-20);
        
    }

    

}

[System.Serializable]
public class Character {
    public string name;
    public Sprite sprite;
    //public string rarity;
}