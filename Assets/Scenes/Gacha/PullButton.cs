using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PullButton : MonoBehaviour
{
    [SerializeField] private List<Character> allCharas;
    private float alphaThreshold = 0.1f;

    private Manager manager;


    private int[] pullWeights = {50, 35, 15};
    private Button button;
    //public int rand;
    public GameObject pulledChara;

    [SerializeField] GameObject thingToHide;

    [SerializeField] TMP_Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager").gameObject.GetComponent<Manager>();
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
        //button = GetComponent<Button>();
        //button.onClick.AddListener(Pull);

        coinsText.text = manager.getCoins() + "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pull() {
        int coins = manager.getCoins();

        //print(coins);

        //if (coins < 20) {
        //    print("not enough coins!");
        //    return;
        //}
        
        if(thingToHide != null) thingToHide.SetActive(false);


            Character hot = allCharas[Random.Range(0,allCharas.Count)];  //the chara u pulled
            

            pulledChara.GetComponent<SpriteRenderer>().sprite = hot.sprite;
            pulledChara.SetActive(true);

            print("u got the lovely " + hot.name);

            pulledChara.GetComponent<PulledChara>().WaitForMouseClick();

            GameObject.FindGameObjectWithTag("manager").gameObject.GetComponent<Manager>().addToCoins(-20);
        coinsText.text = manager.getCoins() + "";
    }

    

}

[System.Serializable]
public class Character {
    public string name;
    public Sprite sprite;
}