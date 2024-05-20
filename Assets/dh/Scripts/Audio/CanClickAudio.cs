using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanClickAudio : MonoBehaviour
{
    Button button;
    public AudioClip audioClip;
    public AudioSource audioSource;
    private void Start()
    {
        button = GetComponent<Button>();
        if (audioSource == null)
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
        }

    }
    public void OnClickSound()
    {
        if (button.interactable)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

}
