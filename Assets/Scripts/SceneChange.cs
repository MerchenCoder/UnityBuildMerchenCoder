using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //홈으로 씬 전환
    public void ChangetoHome()
    {
        SceneManager.LoadScene("Home");
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





}
