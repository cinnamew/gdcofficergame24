using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    private CircleCollider2D circleCollider2D;
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("xp")){
            GetComponentInParent<LevelXPManager>().XPCollected(other);
        }
    }

    public void SetRadius(float newRadius){
        circleCollider2D.radius = newRadius;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
