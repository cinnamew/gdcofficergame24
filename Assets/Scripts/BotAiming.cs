using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAiming : MonoBehaviour
{
    Vector2 aimPoint; // players aim at mouse, enemies with projectiles aim at players, AI buddies aim at enemies
    //AI should have a target priority (e.g. enemies will go for a bait object if a player uses one)
    int aimPriority;
    //e.g. a player will have an aimPriority of 1 but the cherry bomb he deploys will have an aimPriority of 2
    // the enemies will go for whatever object has the higher aimPriority value
    private void Update() {
        //every frame, look at the aimpoint
    }
    void CheckAimPriority() {
        //first check for whatever target is closest. Then check aimPriority value
    }
    //consider detection range
}