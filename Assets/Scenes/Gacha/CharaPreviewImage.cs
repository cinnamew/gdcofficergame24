using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharaPreviewImage : MonoBehaviour
{
    [SerializeField] List<GachaCharacter> charas = new List<GachaCharacter>();
    [SerializeField] List<Image> charaImages = new List<Image>();
    int currImage = 0;
    bool changeImage = true;
    private Image oldImage;
    private Image newImage;

    [SerializeField] TMP_Text oldText;
    [SerializeField] TMP_Text newText;

    void Start() {
        newImage = charaImages[currImage];
        currImage++;
        //if(oldImage.color == null) print("ur mom");
        StartCoroutine(Hi());
        oldText.text = charas[currImage].getName();
    }


    IEnumerator Hi() {
        while(changeImage) {
            if(currImage >= charaImages.Count) currImage = 0;
            //change image
            oldText.text = newText.text;
            newText.text = charas[currImage].getName();
            oldImage = newImage;
            newImage = charaImages[currImage];
            currImage++;
            StartCoroutine(Bye());
            yield return new WaitForSeconds(3);
        }
        //yield return null;
    }

    IEnumerator Bye() {
        //print("fading");
        for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                newImage.color = new Color(1, 1, 1, i);
                newText.color = new Color(1, 1, 1, i);
                oldImage.color = new Color(1, 1, 1, 1 - i);
                oldText.color = new Color(1, 1, 1, 1 - i);

                if(changeImage) yield return null;
            }
    }

    public void nextOrPrevImage(bool next) {
        if(next) {
            currImage++;
            if(currImage >= charaImages.Count) currImage = 0;
            changeToImage(currImage);
        }else {
            currImage--;
            if(currImage < 0) currImage = charaImages.Count - 1;
            changeToImage(currImage);
        }
    }

    public void changeToImage(int a) {
        changeImage = false;
        newImage.color = new Color(1, 1, 1, 0);
        oldImage.color = new Color(1, 1, 1, 0);

        charaImages[a].color = new Color(1, 1, 1, 1);
        oldImage = newImage;
        newImage = charaImages[a];

        //dumb.sprite = charaImages[a];
    }

    public void startImageChange() {
        //if(changeImage) return;
        changeImage = true;
        StartCoroutine(Hi());
    }

    public void SwitchImageChangeStatus() {
        changeImage = !changeImage;
        if(changeImage) startImageChange();
    }
}
