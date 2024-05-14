using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;
    public float totalTime; // 제한시간

    private float currentTime;
    public bool isStarted;
    private CardGameManager cardGameManager;

    public AudioSource audioSource;

    void Start()
    {
        isStarted = false;
        audioSource = GetComponent<AudioSource>();
        cardGameManager = transform.parent.parent.GetComponent<CardGameManager>();
    }

    public void ResetSlider()
    {
        slider = GetComponent<Slider>();
        currentTime = totalTime;
        slider.value = 100;
    }

    public void GameStart()
    {
        isStarted = true;
    }

    void Update()
    {
        if (isStarted)
        {
            currentTime -= Time.deltaTime;

            // Slider의 값 갱신
            slider.value = Mathf.Lerp(0f, 100f, currentTime / totalTime);

            if(currentTime <= 30f && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (currentTime <= 0f)
            {
                // 시간이 다 되었을 때 필요한 작업 수행
                if (cardGameManager != null)
                {
                    Debug.Log("GameOver");
                    isStarted = false;
                    cardGameManager.GameOver();
                }
                else Debug.LogError("Card Game Manager is null");
            }
        }
        
    }
}
