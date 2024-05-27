using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaishak_Hat_Shield : MonoBehaviour
{
    [SerializeField] float expansionSpeed;
    [SerializeField] float desiredScale;
    [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localScale.x < desiredScale)
        {
            gameObject.transform.localScale += Time.deltaTime * new Vector3(expansionSpeed, expansionSpeed, 0);
        }
    }
}
