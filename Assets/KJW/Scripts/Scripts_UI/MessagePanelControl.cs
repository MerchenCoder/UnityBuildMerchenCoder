using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanelControl : MonoBehaviour
{
    public GameObject[] panels = new GameObject[3];
    public Button[] reportButtons = new Button[2];

    public void MessagePanelBtnClick()
    {
        // 챕터 1 진행중
        if(!DataManager.Instance.gameStateData.chapterIsUnlock[1])
        {
            panels[0].SetActive(true);
            if (GameManager.Instance.CheckPlayProgress("Chap1Clear")) reportButtons[0].interactable = true;
        }
        // 챕터 2 진행중
        else if (DataManager.Instance.gameStateData.chapterIsUnlock[1] && !GameManager.Instance.CheckPlayProgress("Chap2Clear"))
        {
            panels[1].SetActive(true);
            if (GameManager.Instance.CheckPlayProgress("Chap2Ending")) reportButtons[1].interactable = true;
        }
        // 챕터 3 대기중
        else if (GameManager.Instance.CheckPlayProgress("Chap2Clear"))
        {
            panels[2].SetActive(true);
        }
    }
}
