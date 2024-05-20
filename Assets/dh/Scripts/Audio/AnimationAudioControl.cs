using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioControl : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Start()
    {
        audioSource = AudioManager.instance.GetComponent<AudioSource>();
    }
    public void PlayAnimationSound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }
}
