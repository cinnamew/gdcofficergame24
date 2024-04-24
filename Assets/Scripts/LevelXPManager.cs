using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelXPManager : MonoBehaviour
{
    public int xp = 0;
    private int maxEXP = 15;
    private int level = 1;
    public int currency = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin")) {
            updateCurrency(10); //10 is a placeholder amt
        } else if (other.CompareTag("xp")) {
            updateXP(10); //10 is a placeholder amt
        }
    }

    public void updateXP(int amt)
    {
        xp += amt;
        updateLevel();
    }

    private void updateLevel()
    {
        if (xp >= maxEXP){
            level += 1;
            maxEXP = maxEXP * 2;
        }
    }

    public void updateCurrency(int amt)
    {
        currency += amt;
    }
}
