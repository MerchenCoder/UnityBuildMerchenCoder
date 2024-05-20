using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeBtn : MonoBehaviour
{
    /// <summary>
    /// 홈으로 씬 전환
    /// </summary>
    public void ChangetoHome()
    {
        SceneChange.Instance.ChangetoHome();
    }

    /// <summary>
    /// 챕터 선택 씬으로 전환
    /// </summary>
    public void ChangetoChapter()
    {
        SceneChange.Instance.ChangetoChapter();
    }

    /// <summary>
    /// 챕터1 스테이지 씬으로 전환
    /// </summary>
    public void ChangetoCh1Stage()
    {
        SceneChange.Instance.ChangetoCh1Stage();
    }

    /// <summary>
    /// 챕터2 스테이지 씬으로 전환
    /// </summary>
    public void ChangetoCh2Stage()
    {
        SceneChange.Instance.ChangetoCh2Stage();
    }

    public void ChangetoGameScene()
    {
        if (!string.IsNullOrEmpty(SceneChange.Instance.beforeScene))
            SceneChange.Instance.ChangeToThisScene(SceneChange.Instance.beforeScene);
        else
            ChangetoHome();
    }
}
