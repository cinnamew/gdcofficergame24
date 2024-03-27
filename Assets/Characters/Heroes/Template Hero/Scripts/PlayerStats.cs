using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsScriptObj", menuName = "ScriptableObjects/PlayerStatsManager")]
public class PlayerStats : ScriptableObject
{
    public int MaxHp {get; private set;}
    public float Atk {get; set;} //the attack damage multiplier
    public float Spd {get; set;} //player speed. SPD
    public float Crt {get; private set;} //percentage chance for critical hit. CRT
    public float Pur {get; private set;} //short for pickup radius
    public float Haste {get; private set;} //attackTime = round( baseAttackTime/(1 + haste%/100% ) )
}
