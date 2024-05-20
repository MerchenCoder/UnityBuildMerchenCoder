using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAudioSetting : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] int audioIndex;

    // Start is called before the first frame update
    void Awake()
    {
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        foreach (AudioManager audioManager in audioManagers)
        {
            if (audioManager.GetComponent<AudioControl>())
            {
                audioSource = audioManager.GetComponent<AudioSource>();

                return;
            }
        }
        // audioSource = FindObjectOfType<AudioManager>().GetComponent<AudioSource>();
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
