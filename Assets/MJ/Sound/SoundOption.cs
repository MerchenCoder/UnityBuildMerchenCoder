using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    private GameObject BGMObject;
    private GameObject SFXObject;

    private AudioSource BGMSound;
    private AudioSource SFXSound;
    // Start is called before the first frame update

    public string soundName;

    void Start()
    {

        BGMObject = GameObject.Find("Audio Source_BGM");
        SFXObject = GameObject.Find("Audio Source_SFX");

        if (BGMObject != null)
        {
            BGMSound = BGMObject.GetComponent<AudioSource>();

            if (BGMSound == null)
            {
                Debug.LogError("BGMSound 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("BGMObject 찾을 수 없습니다.");
        }

        if (SFXObject != null)
        {
            SFXSound = SFXObject.GetComponent<AudioSource>();

            if (SFXSound == null)
            {
                Debug.LogError("SFXSound 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("SFXObject 찾을 수 없습니다.");
        }

    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Now Volume is " + volume);
        if (soundName == "BGM")
        {
            BGMSound.volume = 0.01f * volume;
            Debug.Log("Now BGMVolume is " + BGMSound.volume);
        }
        else if (soundName == "SFX")
        {
            SFXSound.volume = 0.01f * volume;
            Debug.Log("Now SFXVolume is " + SFXSound.volume);
        }
    }

}
