using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Upgrade(){
        if (gameObject.name.Contains("Jolie")){
            gameObject.GetComponent<OrbAttack>().upgradeOrbs();
        } else if (gameObject.name.Contains("Jemi")){
            gameObject.GetComponent<OrbAttack>().upgradeOrbs();
        }
    }
}
