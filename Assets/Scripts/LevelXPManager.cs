using System;
using UnityEngine;

public class LevelXPManager : MonoBehaviour
{
    public int xp = 0;
    private int xpForLevelUp = 79;
    private int level = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("xp")) 
        { 
            System.Random r = new System.Random();
            switch (other.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "xpBlue":
                    updateXP(r.Next(1, 11)); // (inclusive, exclusive)
                    break;   
                case "xpGreen":
                    updateXP(r.Next(11, 20)); // (inclusive, exclusive)
                    break;
                case "xpYellow":
                    updateXP(r.Next(20, 50)); // (inclusive, exclusive)
                    break;
                case "xpOrange":
                    updateXP(r.Next(50, 100)); // (inclusive, exclusive)
                    break;
                case "xpPurple":
                    updateXP(r.Next(100, 200)); // (inclusive, exclusive)
                    break;
                case "xpRed":
                    updateXP(r.Next(200, 400)); // (inclusive, exclusive)
                    break;

            }
        }
    }
    
    public void updateXP(int xpGained) 
    {
        xp += xpGained;
        if (xp >= xpForLevelUp)
        {
            xpForLevelUp = Convert.ToInt32(4 * Math.Pow(level+1, 2.1)) - Convert.ToInt32(Math.Pow(4 * level, 2.1));
            level++;
        }
    }
}
