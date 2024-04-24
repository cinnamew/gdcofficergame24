using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Handles the character's HP. Consider converting to ScriptableObject
    //## test
    
    [SerializeField] private int maxHealth = 100;
    [SerializeField] Slider slider;
    int health;
    
    void Start()
    {
        health = maxHealth;
        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
        }
    }

    public void TakeDamage(int damageVal) {
        health -= damageVal;
        Debug.Log("Took " + damageVal + " damage"); //Temporary before healthbar implementation
        if (slider != null)
        {
            slider.value = health;
        }
        if (health <= 0)
        {
            Destroy(gameObject); //show death particle system
        }
    }
    public void HealHealth(int healVal) {
        health += healVal;
        if (slider != null)
        {
            slider.value = health;
        }
    }
}
