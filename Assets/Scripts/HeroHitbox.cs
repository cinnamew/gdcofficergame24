using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHitbox : GenericHitbox
{
    private void Awake() {
        targetTag = "Enemy";
    }

    private void Start(){ //This is a really stupid solution to the problem; needs to be fixed
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        player = Component.FindObjectOfType<LevelXPManager>().gameObject;
    }
}
