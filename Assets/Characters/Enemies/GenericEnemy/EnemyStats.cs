using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : ScriptableObject
{
    [SerializeField] float maxHealth;
    float currHealth;
    [SerializeField] float defaultMoveSpeed;
    float speedMultiplier; //for status effects that slow down the enemy
    //formula would be defaultMoveSpeed * speedMultiplier
    [SerializeField] float contactDmg;
    [SerializeField] float contactDmgRate;
    [SerializeField] float bodyScaleMultiplier; //for making HUGE enemies
}
