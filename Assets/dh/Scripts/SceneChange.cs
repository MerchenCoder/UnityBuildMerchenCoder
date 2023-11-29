using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    static GameObject container;
    //---싱글톤 선언----//
    static SceneChange instance;
    public static SceneChange Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "SceneManager";
                instance = container.AddComponent(typeof(SceneChange)) as SceneChange;
                DontDestroyOnLoad(container);

            }
            return instance;

        }
    }






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

    //각 스테이지 씬으로 전환
    public void ChangetoCh1S1()
    {
        SceneManager.LoadScene("Ch1S1");
    }
    public void ChangetoCh1S2()
    {
        SceneManager.LoadScene("Ch1S2");
    }
    public void ChangetoCh1S3()
    {
        SceneManager.LoadScene("Ch1S3");
    }
    public void ChangetoCh1S4()
    {
        SceneManager.LoadScene("Ch1S4");
    }
    public void ChangetoCh1S5()
    {
        SceneManager.LoadScene("Ch1S5");
    }
    public void ChangetoCh1S6()
    {
        SceneManager.LoadScene("Ch1S6");
    }

    public void ChangeToThisScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
