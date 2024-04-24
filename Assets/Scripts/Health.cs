using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Handles the character's HP. Consider converting to ScriptableObject
    //## test
    
    [SerializeField] private int maxHealth = 100;
    int health;
    
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageVal) {
        health -= damageVal;
        Debug.Log("Took " + damageVal + " damage"); //Temporary before healthbar implementation
        if (health <= 0){
            Destroy(gameObject); //show death particle system
        }
    }
    public void HealHealth(int healVal) {
        health += healVal;
    }
}
