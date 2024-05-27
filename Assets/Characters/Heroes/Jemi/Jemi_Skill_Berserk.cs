using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jemi_Skill_Berserk : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeSpan;
    public int numProjectilesToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < numProjectilesToSpawn; i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
