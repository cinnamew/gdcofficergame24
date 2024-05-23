using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaPreviewImage : MonoBehaviour
{
    [SerializeField] List<Image> charaImages = new List<Image>();
    int currImage = 0;
    bool changeImage = true;
    private Image oldImage;
    private Image newImage;

    void Start() {
        newImage = charaImages[currImage];
        currImage++;
        //if(oldImage.color == null) print("ur mom");
        StartCoroutine(Hi());
    }


    IEnumerator Hi() {
        while(changeImage) {
            if(currImage >= charaImages.Count) currImage = 0;
            //change image
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
                oldImage.color = new Color(1, 1, 1, 1 - i);

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
