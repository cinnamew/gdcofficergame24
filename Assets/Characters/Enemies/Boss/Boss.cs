using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shotInterval = 6f;

    private Vector3[] arr = new Vector3[4];
    private float timer;
    private void Start()
    {
        arr[0] = new Vector2(0, 0); arr[1] = new Vector2(1, 0); arr[2] = new Vector2(0, 1); arr[0] = new Vector2(1, 1);
    }

    public void SetShotInterval(float newVal){
        shotInterval = newVal;
    }
    void Update()
    {
        if(timer < 0) {
            timer = shotInterval;
            for(int i = 0; i < 4; i++) {
                Instantiate(projectile, transform.position + arr[i] * 3, transform.rotation);
            }

        }
         
        timer -= Time.deltaTime;    
    }
}
