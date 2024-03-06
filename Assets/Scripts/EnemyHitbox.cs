using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : GenericHitbox
{
    private void Awake() {
        targetTag = "Hero";
    }
}
