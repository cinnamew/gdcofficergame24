using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

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
