using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkBtnAfterSuccess : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ChangeSceneAfterMissionClear);
    }

    //문제풀이 씬에서 미션 깨고 난 후 씬 전환
    public void ChangeSceneAfterMissionClear()
    {
        string beforeScene = SceneChange.Instance.beforeScene;
        if (beforeScene == "Home")
        {
            //복습하기 모드에서 문제 씬으로 이동한 경우
            Debug.Log("복습하기 모드");
            SceneChange.Instance.ChangeToThisScene("Home");
        }
        else if (beforeScene == null)
        {
            Debug.Log(beforeScene);
            Debug.Log("beforeScene이 없는 상태. 개발 중인 상태");
        }
        else
        {
            Debug.Log($"학습하기 모드. 이전 씬은 {beforeScene}입니다.");
            SceneChange.Instance.ChangeToThisScene(beforeScene);
        }

    }


}
