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
    private Slider slider;
    int health;
    [SerializeField] ParticleSystem deathSmoke;
    [SerializeField] GameObject[] XPOrbs;
    [SerializeField] GameObject coin;
    Vector3 victimVector3;
    AnimController animController;
    [SerializeField] GameObject dmgIndicator;
    private StatsManager statsManager;
    private UpgradeInventory upgradeInventory;
    private bool debuggingHell;
    private bool enemiesToLovers;
    private bool empathy;

    [SerializeField] bool isBoss = false;
    
    void Start()
    {
        //if(Random.Range(0,9) == 5) isBoss = true;
        //victimVector3 = GetComponent<Vector3>(); commented out bc it was causing an error
        animController = GetComponent<AnimController>();
        if (gameObject.tag == "Player"){
            statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
            maxHealth = statsManager.MaxHp;
            slider = GameObject.Find("Canvas").GetComponent<StageUIManager>().GetHealthSlider();
        }
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

    void Update(){
        if (gameObject.tag == "Player" && slider != null){
            maxHealth = statsManager.MaxHp;
            slider.maxValue = maxHealth;
        }
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
            ParticleSystem deathVFX = Instantiate(deathSmoke, transform.position, Quaternion.identity);
            deathVFX.Play();
            
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

                //coin spawning!
                if(Random.Range(0,5) == 1) {
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
                if(isBoss) {
                    for(int i = 0; i < 15; i++) {
                        Instantiate(coin, new Vector3(transform.position.x + 0.1f * Random.Range(-i, 20), transform.position.y + 0.1f * Random.Range(-i, 20), 1), Quaternion.identity);
                    }
                }
            }
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f); //DONT DESTROY PLAYER
            Destroy(deathVFX.gameObject, 0.5f);
            
        }
    }

    public void ConditionalHeal()
    {
        if (empathy)
        {
            
        }
        if (debuggingHell)
        {
            
        }
        if (enemiesToLovers)
        {

        }
    }

    IEnumerator TimeCrunch(){
        
        UpgradeItem timeCrunch = upgradeInventory.GetItemWithName("Time Crunch");
        timeCrunch.ApplyBuffs(statsManager);
        yield return new WaitForSeconds(timeCrunch.BuffTime);
        timeCrunch.UnapplyBuffs(statsManager);
    }
    public void HealHealth(int healVal) {
        health = Mathf.Min(health + healVal, maxHealth);
        if (slider != null)
        {
            slider.value = health;
        }
    }
}
