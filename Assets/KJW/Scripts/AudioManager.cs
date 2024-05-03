using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup backgroundMusicMixerGroup; // 배경음악을 위한 Audio Mixer 그룹
    public AudioMixerGroup soundEffectMixerGroup; // 효과음을 위한 Audio Mixer 그룹

    // 배경음악과 효과음을 위한 오디오 소스
    private AudioSource backgroundMusicSource;
    private AudioSource[] soundEffectSources;

    private Coroutine fadingCoroutine; // 페이드 아웃/인 코루틴을 관리하기 위한 변수

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 오디오 소스 초기화
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.outputAudioMixerGroup = backgroundMusicMixerGroup;

        // 효과음을 위한 오디오 소스 배열 초기화
        soundEffectSources = new AudioSource[3];
        for (int i = 0; i < soundEffectSources.Length; i++)
        {
            soundEffectSources[i] = gameObject.AddComponent<AudioSource>();
            soundEffectSources[i].outputAudioMixerGroup = soundEffectMixerGroup;
        }
    }

    // 배경음악 재생
    public void PlayBackgroundMusic(AudioClip clip, float volume, float fadeDuration = 1f)
    {
        if (backgroundMusicSource.clip == clip && backgroundMusicSource.isPlaying)
        {
            return; // 이미 같은 곡이 재생 중인 경우 무시
        }

        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine); // 현재 페이드 아웃 중인 경우 중지
        }

        fadingCoroutine = StartCoroutine(FadeOutAndIn(clip, volume, fadeDuration));
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
        backgroundMusicSource.volume = startVolume;

        // 새로운 배경음악 재생
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.volume = 0f;
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


    // 배경음악 정지
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    // 효과음 재생
    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        // 효과음을 차례로 재생하며, 비어있는 AudioSource를 찾아 사용함
        foreach (var source in soundEffectSources)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volume;
                source.Play();
                return;
            }
        }
        Debug.LogWarning("모든 AudioSource가 사용 중입니다. 효과음을 재생할 수 없습니다.");
    }
}
