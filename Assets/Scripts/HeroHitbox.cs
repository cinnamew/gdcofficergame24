using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHitbox : GenericHitbox
{
    private void Awake() {
        targetTag = "Enemy";
    }
}
