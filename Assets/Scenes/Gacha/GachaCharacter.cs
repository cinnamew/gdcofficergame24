using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaCharacter : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] string name;

    [SerializeField] string bio;
    [SerializeField] int hp;
    [SerializeField] float attack;
    [SerializeField] float crit;
    [SerializeField] int spd;
    
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getStats() {
        string temp = "";
        if(Manager.Obj.hasCharacter(name) == 0) {
            temp = "HP: ?\nATK: ?\nCRT: ?\nSPD: ?";
        }else {
            temp = "HP: " + hp + " -> ?" + "\nATK: " + attack + " -> ?" + "\nCRT: " + crit + " -> ?" + "\nSPD: " + spd + " -> ?";
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
