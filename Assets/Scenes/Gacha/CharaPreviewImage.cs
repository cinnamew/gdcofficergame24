using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharaPreviewImage : MonoBehaviour
{
    [SerializeField] List<GachaCharacter> charas = new List<GachaCharacter>();
    [SerializeField] List<Image> charaImages = new List<Image>();
    [SerializeField] int currImage = 0;
    bool changeImage = true;
    private Image oldImage;
    private Image newImage;

    private bool ductTape = false;  //if we are waiting for next coroutine

    [SerializeField] TMP_Text oldText;
    [SerializeField] TMP_Text newText;
    [SerializeField] TMP_Text oldStats;
    [SerializeField] TMP_Text newStats;
    [SerializeField] TMP_Text oldBio;
    [SerializeField] TMP_Text newBio;
    [SerializeField] TMP_Text oldNum;
    [SerializeField] TMP_Text newNum;

    private Manager manager;

    [SerializeField] PauseCharas pauseButton;

    void Start() {
        newImage = charaImages[currImage];
        currImage++;
        //if(oldImage.color == null) print("ur mom");   //when did i add this??? did i even add this??? this is not a me sentence
        oldText.text = charas[0].getName();
        oldStats.text = charas[0].getStats();
        oldBio.text = charas[0].getBio();
        manager = GameObject.FindGameObjectWithTag("manager").gameObject.GetComponent<Manager>();
        oldNum.text = "x" + manager.hasCharacter(charas[0].getName());
        StartCoroutine(Hi());
        //print(currImage);
    }

    public void Reset() {
        for(int i = 0; i < charaImages.Count; i++) {
            Image temp = charaImages[i];
            if(i == currImage) temp.color = new Color(1,1,1,1);
            else temp.color = new Color(1,1,1,0);
        }
    }

    public void StartHi() {
        changeImage = true;
        StartCoroutine(Hi());
    }

    IEnumerator Hi() {
        ductTape = false;
        while(changeImage) {
            if(currImage >= charaImages.Count) currImage = 0;
            //print("current image: " + currImage);

            //change image
            oldText.text = newText.text;
            newText.text = charas[currImage].getName();
            print("CAROUSEL old text: " + oldText.text + " || currImage: " + currImage);

            oldStats.text = newStats.text;
            newStats.text = charas[currImage].getStats();
            
            oldBio.text = newBio.text;
            newBio.text = charas[currImage].getBio();

            oldNum.text = newNum.text;
            newNum.text = "x" + manager.hasCharacter(charas[currImage].getName());

            oldImage = newImage;
            newImage = charaImages[currImage];
            currImage++;
            if(!changeImage) {
                break;
            }
            StartCoroutine(Bye());

            ductTape = true;
            yield return new WaitForSeconds(3);
        }
        ductTape = false;
        yield return null;
    }

    IEnumerator Bye() {
        //print("fading");
        for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                newImage.color = new Color(1, 1, 1, i);
                newText.color = new Color(1, 1, 1, i);
                oldImage.color = new Color(1, 1, 1, 1 - i);
                oldText.color = new Color(1, 1, 1, 1 - i);

                oldStats.color = new Color(0,0,0,1-i);
                newStats.color = new Color(0,0,0,i);
                oldBio.color = new Color(0,0,0,1-i);
                newBio.color = new Color(0,0,0,i);
                oldNum.color = new Color(0,0,0,1-i);
                newNum.color = new Color(0,0,0,i);

                if(changeImage) yield return null;
            }
    }

    public void nextOrPrevImage(bool next) {
        if(next) {
            if(!ductTape) {
                currImage++;
            }else ductTape = false;
            if(currImage >= charaImages.Count) currImage = 0;
            changeToImage(currImage);
        }else {
            currImage--;
            if(currImage < 0) currImage = charaImages.Count - 1;
            changeToImage(currImage);
        }
        pauseButton.SwitchToPlay();
    }

    public void changeToImage(int a) {
        changeImage = false;
        newImage.color = new Color(1, 1, 1, 0);
        oldImage.color = new Color(1, 1, 1, 0);
        oldText.color = new Color(1,1,1,1);
        newText.color = new Color(1,1,1,0);

        oldBio.color = new Color(0,0,0,0);
        newBio.color = new Color(0,0,0,1);
        oldStats.color = new Color(0,0,0,0);
        newStats.color = new Color(0,0,0,1);
        
        oldText.text = charas[a].getName();
        charaImages[a].color = new Color(1, 1, 1, 1);
        oldImage = newImage;
        newImage = charaImages[a];
        oldBio = newBio;
        oldStats = newStats;
        newStats.text = charas[currImage].getStats();
        newBio.text = charas[currImage].getBio();
        oldNum = newNum;
        newNum.text = "x" + manager.hasCharacter(charas[currImage].getName());

        print("old text: " + oldText.text + " || currImage: " + currImage);
    }

    public void startImageChange() {
        //if(changeImage) return;
        changeImage = true;
        //StopCoroutine(Hi());
        if(!ductTape) StartCoroutine(Hi());
        //print("starting coroutine from FUNCTION");
    }

    public void SwitchImageChangeStatus() {
        //print("swtich image ahcange cstatus");
        changeImage = !changeImage;
        if(changeImage) startImageChange();
    }

    public bool GetImageChangeStatus() {
        return changeImage;
    }
}
