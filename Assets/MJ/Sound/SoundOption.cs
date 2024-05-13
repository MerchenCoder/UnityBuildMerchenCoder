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

    private string soundName;

    private Slider BGMSlider;
    private Slider SFXSlider;

    void Start()
    {

        BGMObject = GameObject.Find("Audio Source_BGM");
        SFXObject = GameObject.Find("Audio Source_SFX");

        BGMSlider = GameObject.Find("Slider Lightpurple").GetComponent<Slider>();
        SFXSlider = GameObject.Find("Slider Pinkpurple").GetComponent<Slider>(); ;

        if (BGMObject != null)
        {
            BGMSound = BGMObject.GetComponent<AudioSource>();

            if (BGMSound == null)
            {
                Debug.LogError("BGMSound 컴포넌트를 찾을 수 없습니다.");
            }
            else
            {
                BGMSlider.value = BGMSound.volume * 100;
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
            else
            {
                SFXSlider.value = SFXSound.volume * 100;
            }
        }
        else
        {
            Debug.LogError("SFXObject 찾을 수 없습니다.");
        }

        //Debug.Log("Object name is " + this.gameObject.name);

        if (this.gameObject.name == "Slider Lightpurple")
        {
            soundName = "BGM";
        }
        else if (this.gameObject.name == "Slider Pinkpurple")
        {
            soundName = "SFX";
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
