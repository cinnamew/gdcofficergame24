using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCharas : MonoBehaviour
{
    [SerializeField] CharaPreviewImage charaPreviewImage;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject play;
    private bool playing = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchSceneYaay() {
        Manager.Obj.goToPreviousMenu();
    }

    public void PausePlayChara() {
        charaPreviewImage.GetComponent<CharaPreviewImage>().SwitchImageChangeStatus();
        playing = !playing;
        if(playing) {
            pause.SetActive(true);
            play.SetActive(false);
        }else {
            SwitchToPlay();
        }
    }

    public void SwitchToPlay() {
        play.SetActive(true);
        pause.SetActive(false);
        playing = true;
    }
}
