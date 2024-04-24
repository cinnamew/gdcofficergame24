using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    private void Start() {
        smokeEffect.Play();
    }
}
