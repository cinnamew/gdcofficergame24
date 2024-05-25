using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet_PlayVFX : MonoBehaviour
{
    [SerializeField] GameObject vfx;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = Instantiate(vfx, transform.position, Quaternion.identity);
        temp.GetComponent<Animator>().Play("jemian_pellet_hit");
        StartCoroutine(destroyVFX(temp));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator destroyVFX(GameObject t)
    {
        yield return new WaitForSeconds(1);
        Destroy(t);

    }
}
