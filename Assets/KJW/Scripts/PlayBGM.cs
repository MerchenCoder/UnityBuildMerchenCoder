using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            AudioManager.instance.PlayBackgroundMusic(audioSource);
        }
    }

}
