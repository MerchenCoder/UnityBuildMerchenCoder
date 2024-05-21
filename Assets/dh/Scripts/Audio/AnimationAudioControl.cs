using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioControl : MonoBehaviour
{
    private bool isPlaying = false;

    [SerializeField] private AudioSource audioSource;
    public AudioClip[] audioClips;
    public void PlayAnimationSound(int index)
    {
        if (isPlaying) return;
        isPlaying = true;
        audioSource.PlayOneShot(audioClips[index]);
    }
    public void PlayAnimationSound_Loop(int index)
    {
        if (isPlaying) return;
        isPlaying = true;
        audioSource.clip = audioClips[index];
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopAnimationSound()
    {
        isPlaying = false;
        audioSource.Stop();
        audioSource.loop = false;

    }

    public void SetPlaySpeed(int pitch)
    {
        audioSource.pitch = pitch;
    }


}
