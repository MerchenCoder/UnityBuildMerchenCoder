using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAudioSetting : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] int audioIndex;
    public AudioSource AudioSource => audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        if (audioSource != null) return;
        AudioControl audioControl = FindObjectOfType<AudioControl>();

        audioSource = audioControl.GetComponent<AudioSource>();
    }

    public void OnClickSound()
    {
        audioSource.GetComponent<AudioControl>().SoundPlayOneShot(audioIndex);
    }
    public void OnClickSound_Index(int index)
    {
        audioSource.GetComponent<AudioControl>().SoundPlayOneShot(index);
    }

}
