using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundEffectClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void SoundPlayOneShot(int index)
    {
        audioSource.PlayOneShot(soundEffectClips[index]);
    }
}
