using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Handles the character's HP. Consider converting to ScriptableObject
    int health;
    
    void Start()
    {
        health = 1;
    }

    public void TakeDamage(int damageVal) {
        health -= damageVal;
    }
    public void HealHealth(int healVal) {
        health += healVal;
    }
}
