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
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName() {
        return name;
    }
}
