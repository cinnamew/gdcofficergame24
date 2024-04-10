using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAiming : MonoBehaviour
{
    public Vector2 aimPoint; // players aim at mouse, enemies with projectiles aim at players, AI buddies aim at enemies
    //AI should have a target priority (e.g. enemies will go for a bait object if a player uses one)
    [SerializeField] public int aimPriority;
    [SerializeField] private bool enableAim = true;
    //e.g. a player will have an aimPriority of 1 but the cherry bomb he deploys will have an aimPriority of 2
    // the enemies will go for whatever object has the higher aimPriority value
    private void FixedUpdate()
    {
        if (enableAim){
            //every frame, look at the aimpoint
            CheckAimPriority();
            Vector2 direction = aimPoint - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
    }
    public virtual void CheckAimPriority() {
        //first check for whatever target is closest. Then check aimPriority value
    }
    //consider detection range
}