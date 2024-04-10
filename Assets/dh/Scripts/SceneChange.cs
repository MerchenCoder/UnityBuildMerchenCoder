using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class SceneChange : MonoBehaviour
{
    private EventSystem eventSystem;

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
        if (SceneManager.GetActiveScene().name == "Chapter")
            SceneManager.LoadScene("Home");
        else // 플레이씬의 경우 페이드아웃 - 페이드인
        {
            StartCoroutine(ChangeSceneDelay("Home"));
        }
    }

    //챕터 선택 씬으로 전환
    public void ChangetoChapter()
    {
        // SceneManager.LoadScene("Chapter");
        SceneManager.LoadSceneAsync("Chapter");
    }

    //챕터별 스테이지 씬으로 전환
    public void ChangetoCh1Stage()
    {
        // if(GameManager.Instance.gameObject.TryGetComponent<PlayData>(out PlayData playData))
        // {
        //     if (playData.nowPlayPointIndex <= 10) ChangeToThisScene("1_1_farmer");
        //     else if (playData.nowPlayPointIndex <= 24) ChangeToThisScene("1_2_town");
        //     else if (playData.nowPlayPointIndex <= 31) ChangeToThisScene("1_3_castle");
        //     else if (playData.nowPlayPointIndex <= 35) ChangeToThisScene("1_4_forest");
        // }
        // //SceneManager.LoadSceneAsync("Ch1Stage");
        ChangeToThisScene(GameManager.Instance.playerData.chapterCurrentScene[0]);
    }
    public void ChangetoCh2Stage()
    {
        SceneManager.LoadSceneAsync("Ch2Stage");
    }
    public void ChangetoCh3Stage()
    {
        SceneManager.LoadSceneAsync("Ch3Stage");
    }

    public void ChangeToThisScene(string SceneName)
    {
        beforeScene = SceneManager.GetActiveScene().name;
        StartCoroutine(ChangeSceneDelay(SceneName));
    }

    IEnumerator ChangeSceneDelay(string SceneName)
    {
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.enabled = false;
            Debug.LogFormat("<color=red>캔버스 이벤트 비활성화</color>");
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                break;

            }
        }
        fadePanel.FadeOut();
        yield return new WaitForSeconds(1f); // Wait Fading Time
        asyncLoad.allowSceneActivation = true;
        yield return new WaitForSeconds(0.3f);
        fadePanel.FadeIn();
    }
}
