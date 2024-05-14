using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup backgroundMusicMixerGroup; // 배경음악을 위한 Audio Mixer 그룹
    public AudioMixerGroup soundEffectMixerGroup; // 효과음을 위한 Audio Mixer 그룹

    public AudioSource backgroundMusicSource;

    private float BG_Volume = 1f;

    //// 싱글톤 패턴 적용
    //public AudioManager Instance()
    //{
    //    if (instance == null)
    //    {
    //        instance = new AudioManager();
    //        instance.backgroundMusicMixerGroup = Resources.Load("Sounds/BGM_AM") as AudioMixerGroup;
    //        instance.soundEffectMixerGroup = Resources.Load("Sounds/SFX_AM") as AudioMixerGroup;
    //        DontDestroyOnLoad(this);
    //    }
    //    return instance;
    //}

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
            backgroundMusicSource = GetComponent<AudioSource>();
            backgroundMusicSource.outputAudioMixerGroup = backgroundMusicMixerGroup;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetBGMVolume(float volume)
    {
        backgroundMusicMixerGroup.audioMixer.SetFloat("Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        soundEffectMixerGroup.audioMixer.SetFloat("Volume", volume);
    }

    // 배경음악 재생
    public void PlayBackgroundMusic(AudioSource audioSource, float fadeDuration = 0.5f)
    {
        if (backgroundMusicSource.clip == audioSource.clip && backgroundMusicSource.isPlaying)
        {
            return; // 이미 같은 곡이 재생 중인 경우 무시
        }
        if (backgroundMusicSource.clip == null)
        {
            backgroundMusicSource.clip = audioSource.clip;
            backgroundMusicSource.Play();
            return;
        }
        if (backgroundMusicSource.clip != audioSource.clip) StartCoroutine(FadeOutAndIn(audioSource.clip, BG_Volume, fadeDuration));
    }

    private IEnumerator FadeOutAndIn(AudioClip clip, float volume, float fadeDuration)
    {
        float startVolume = backgroundMusicSource.volume;
        float timer = 0f;

        // 페이드 아웃
        while (timer < fadeDuration)
        {
            backgroundMusicSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        backgroundMusicSource.Stop();

        // 새로운 배경음악 재생
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.Play();

        // 페이드 인
        timer = 0f;
        while (timer < fadeDuration)
        {
            backgroundMusicSource.volume = Mathf.Lerp(0f, volume, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        backgroundMusicSource.volume = volume;
    }
}
