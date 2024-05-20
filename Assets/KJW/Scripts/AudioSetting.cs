using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSetting : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public GameObject settingPanel;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Node")
        {
            Debug.Log("노드 씬은 설정창이 없기에 로컬에 저장된 볼륨 설정값을 그대로 가져와 적용한다.");
            float bgmValue = PlayerPrefs.GetFloat("BGMVolume");
            float sfxValue = PlayerPrefs.GetFloat("SFXVolume");
            SetBGMVolume(bgmValue);
            SetSFXVolume(sfxValue);
            return;
        }

        settingPanel.SetActive(true);

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else BGMSlider.value = 50f;
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else SFXSlider.value = 50f;

        SetBGMVolume(BGMSlider.value);
        SetSFXVolume(SFXSlider.value);
        settingPanel.SetActive(false);
    }

    private float SliderToDecibel(float sliderValue)
    {
        float normalizedValue = sliderValue / 100f;
        return Mathf.Log10(Mathf.Clamp(normalizedValue, 0.0001f, 1f)) * 20;
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGMVolume", volume);
        float dB = SliderToDecibel(volume);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetBGMVolume(dB);
        }
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        float dB = SliderToDecibel(volume);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSFXVolume(dB);
        }
    }
}