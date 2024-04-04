using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NodeMissionBtn : MonoBehaviour
{
    public GameObject MissionInfoPanel;
    public bool isDone = false;

    Button button;

    void Start()
    {
        MissionInfoPanel = transform.GetChild(1).gameObject;

        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners(); // 기존 리스너 제거
        button.onClick.AddListener(MissionPanelActive);
    }

    public void MissionPanelActive()
    {
        if (!isDone)
        {
            //가져온 미션 정보로 mission panel 내부 셋팅하기

            MissionInfoPanel.GetComponent<MissionManager>().MissionInfoSetting();
            isDone = true; //한 번 셋팅하고 나면 다시 셋팅 안해도 되도록 처리
        }

        MissionInfoPanel.SetActive(true);
        button.interactable = false;

    }

    public void RestIsDone()
    {
        isDone = false;
    }

}
