using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PulledChara : MonoBehaviour
{
     [SerializeField] List<GameObject> showAfterDone = new List<GameObject>();
     [SerializeField] List<GameObject> hideAfterDone = new List<GameObject>();

     [SerializeField] CharaPreviewImage charaPreviews;
     [SerializeField] TMP_Text nameText;
     [SerializeField] TMP_Text xText;

    public void WaitForMouseClick() {
        StartCoroutine(Dense());
    }

    void OnEnable() {
        //if(charaPreviews.GetImageChangeStatus()) charaPreviews.Reset();
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
                    if(charaPreviews != null) charaPreviews.startImageChange();
                }
            }
            yield return null;
        }
    }
}
