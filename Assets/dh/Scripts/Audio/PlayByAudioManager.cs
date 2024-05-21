using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayByAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] audioClips;

    public void PlaySFXByAudioManager(int index)
    {
        if (audioSource == null)
            audioSource = AudioManager.instance.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[index]);
    }
}
