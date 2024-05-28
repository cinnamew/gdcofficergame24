using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsScriptObj", menuName = "ScriptableObjects/PlayerStatsManager")]
public class PlayerStats : ScriptableObject
{
    public string name;
    public int MaxHp;
    public float Atk; //the attack damage multiplier
    public float Spd; //player speed. SPD
    public float Crt; //percentage chance for critical hit. CRT
    public float Pur; //short for pickup radius
    public float Haste; //attackTime = round( baseAttackTime/(1 + haste%/100% ) )
}
