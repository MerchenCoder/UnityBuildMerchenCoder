using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //---싱글톤 선언----//
    public static SceneChange Instance;
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] FadeInOut fadePanel;
    [NonSerialized] public string beforeScene;

    //홈으로 씬 전환
    public void ChangetoHome()
    { 
        if(SceneManager.GetActiveScene().name == "Chapter")
            SceneManager.LoadScene("Home");
        else // 플레이씬의 경우 페이드아웃 - 페이드인
        {
            fadePanel.FadeOut();
            Invoke("ChangeToHomeSceneDelay", 1f);
        }
    }

    // 페이드 추가를 위한 딜레이용 함수
    public void ChangeToHomeSceneDelay()
    {
        SceneManager.LoadScene("Home");
        if (beforeScene != "Chapter") fadePanel.FadeIn();
    }

    //챕터 선택 씬으로 전환
    public void ChangetoChapter()
    {
        SceneManager.LoadScene("Chapter");
    }

    //챕터별 스테이지 씬으로 전환
    public void ChangetoCh1Stage()
    {
        SceneManager.LoadScene("Ch1Stage");
    }
    public void ChangetoCh2Stage()
    {
        SceneManager.LoadScene("Ch2Stage");
    }
    public void ChangetoCh3Stage()
    {
        SceneManager.LoadScene("Ch3Stage");
    }

    public void ChangeToThisScene(string SceneName)
    {
        beforeScene = SceneManager.GetActiveScene().name;
        StartCoroutine(ChangeSceneDelay(SceneName));
    }

    IEnumerator ChangeSceneDelay(string SceneName)
    {
        fadePanel.FadeOut();
        yield return new WaitForSeconds(1f); // Wait Fading Time
        SceneManager.LoadScene(SceneName);
        fadePanel.FadeIn();
        yield return null;
    }

}
