using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCharas : MonoBehaviour
{
    [SerializeField] CharaPreviewImage charaPreviewImage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausePlayChara() {
        charaPreviewImage.GetComponent<CharaPreviewImage>().SwitchImageChangeStatus();

    }
}
