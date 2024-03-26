using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionManager : MonoBehaviour
{
    private string missionTitle;
    private string missionInfo;
    // Start is called before the first frame update

    GameObject title;
    //public string mode = "main";
    GameObject rewards;
    GameObject info;
    void Start()
    {


    }



    public void MissionInfoSetting()
    {


        title = transform.GetChild(0).GetChild(0).gameObject;
        rewards = transform.GetChild(0).GetChild(3).gameObject;
        info = transform.GetComponentInChildren<NodeLabelControl>(true).transform.parent.GetChild(0).gameObject;


        missionTitle = "문제 " + GameManager.Instance.missionData.missionCode + " : " + GameManager.Instance.missionData.missionTitle;
        missionInfo = GameManager.Instance.missionData.missionInfo;

        Debug.Log(title.name);
        title.GetComponent<TextMeshProUGUI>().text = missionTitle;
        rewards.transform.GetChild(2).GetComponent<Text>().text = GameManager.Instance.missionData.reward.ToString();
        info.GetComponentInChildren<TextMeshProUGUI>().text = missionInfo;
        transform.GetComponentInChildren<NodeLabelControl>(true).NodeLabelSetting();

        info.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false;
        info.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;

        // LayoutRebuilder.ForceRebuildLayoutImmediate(info.transform.parent.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        Debug.Log("미션 정보 설정 완료");
    }


    public void SceneChangeToNode(string mode)
    {

        if (mode == "main")
        {
            Debug.Log("모드 = 메인");
            SceneChange.Instance.ChangeToThisScene("Node");
        }
        else if (mode == "review")
        {
            Debug.Log("모드 == 복습하기");
            SceneChange.Instance.ChangeToThisScene("Node");
        }
        else
        {
            Debug.Log("튜토리얼");
            //튜토리얼 부분으로 이동할 코드 작성
        }
    }
}
