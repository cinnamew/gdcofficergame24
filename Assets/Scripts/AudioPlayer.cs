using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{

    [SerializeField] AudioClip[] audioClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAudioClip(int element){
        audioSource.PlayOneShot(audioClips[element]);
    }
    public void playAudioClip(int element, float volume){
        audioSource.PlayOneShot(audioClips[element], volume);
    }


}
