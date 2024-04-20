using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulledChara : MonoBehaviour
{
     [SerializeField] List<GameObject> showAfterDone = new List<GameObject>();
     [SerializeField] List<GameObject> hideAfterDone = new List<GameObject>();

    public void WaitForMouseClick() {
        StartCoroutine(Dense());
    }
    
    public void HideObjects() {
        foreach(GameObject g in showAfterDone) {
            g.SetActive(false);
        }
        foreach(GameObject g in hideAfterDone) {
            g.SetActive(true);
        }
    }

    IEnumerator Dense() {
        while(true) {
            if(Input.GetMouseButtonDown(0)) {
                foreach(GameObject g in hideAfterDone) {
                    g.SetActive(false);
                }

                 foreach(GameObject g in showAfterDone) {
                    g.SetActive(true);
                }
            }
            yield return null;
        }
    }
}
