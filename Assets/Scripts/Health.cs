using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    //Handles the character's HP. Consider converting to ScriptableObject
    //## test
    
    [SerializeField] private int maxHealth = 100;
    [SerializeField] Slider slider;
    int health;
    [SerializeField] ParticleSystem deathSmoke;
    [SerializeField] GameObject[] XPOrbs;
    Vector3 victimVector3;
    AnimController animController;
    [SerializeField] GameObject dmgIndicator;
    [SerializeField] PlayerStats playerStats;
    private UpgradeInventory upgradeInventory;
    
    void Start()
    {
        //victimVector3 = GetComponent<Vector3>(); commented out bc it was causing an error
        animController = GetComponent<AnimController>();
        health = maxHealth;
        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
        }
        upgradeInventory = gameObject.GetComponent<UpgradeInventory>();
    }

    public int GetHealth() {
        return health;
    }
    public void TakeDamage(int damageVal) {
        if (gameObject.tag == "Player" && health > (int)(0.2*maxHealth) && health - damageVal < (int)(0.2*maxHealth) && upgradeInventory.HasItemWithName("Time Crunch")){ //change this to be dependent on max health
            StartCoroutine(TimeCrunch());
        }
        health -= damageVal;
        Debug.Log("Took " + damageVal + " damage"); //Temporary before healthbar implementation
        if (slider != null)
        {
            slider.value = health;
        }
        if(gameObject.tag == "Enemy") //assuming we don't want dmg indicator numbers on the player bc there is alr a health bar
        {
            if(dmgIndicator != null)
            {
                GameObject i = Instantiate(dmgIndicator, transform.position, Quaternion.identity);
                i.transform.parent = null;
                i.GetComponent<TMP_Text>().text = "" + damageVal;
            }
        }

        if (health <= 0)
        {
            // Instantiate(deathSmoke, victimVector3, Quaternion.identity);
            // deathSmoke.transform.SetParent(null, true);
            // deathSmoke.Play();
            //Destroy(gameObject); //show death particle system
            //play particle effects
            if (animController != null) {
                animController.isAlive = false;
                if (animController.AnimHasEnded()) {
                    Destroy(gameObject);
                }
            }
            //play death animation (if applicable)
            if (gameObject.tag == "Enemy"){
                GameObject randomXPOrb = XPOrbs[Random.Range(0, XPOrbs.Length)];
                Instantiate(randomXPOrb, transform.position, Quaternion.identity);
                Debug.Log("Upgraded Orb");
            }
            Destroy(gameObject); //show death particle system DONT DESTROY PLAYER
        }
    }

    IEnumerator TimeCrunch(){
        
        UpgradeItem timeCrunch = upgradeInventory.GetItemWithName("Time Crunch");
        timeCrunch.ApplyBuffs(playerStats);
        yield return new WaitForSeconds(timeCrunch.BuffTime);
        timeCrunch.UnapplyBuffs(playerStats);
    }
    public void HealHealth(int healVal) {
        health += healVal;
        if (slider != null)
        {
            slider.value = health;
        }
    }
}
