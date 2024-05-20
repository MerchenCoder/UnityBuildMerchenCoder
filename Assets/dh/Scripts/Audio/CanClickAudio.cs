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
            AudioControl audioControl = FindObjectOfType<AudioControl>();
            audioSource = audioControl.GetComponent<AudioSource>();
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
