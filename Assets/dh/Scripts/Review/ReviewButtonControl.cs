using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewButtonControl : MonoBehaviour
{
    int idx = 0;
    private void Start()
    {
        SetCh1ButtonUnlock(0);
        Debug.Log(idx);
        SetCh2ButtonUnlock(idx);
        Debug.Log(idx);
    }

    //복습하기 onclick시 호출하도록(디버그 버튼에 클릭시 데이터 변경 즉각적 반영 위함)
    public void UpdateStageUnlock()
    {
        idx = 0;
        SetCh1ButtonUnlock(0);
        SetCh2ButtonUnlock(idx);
    }


    public void SetCh1ButtonUnlock(int childStartIndex)
    {
        idx = childStartIndex;
        Debug.Log(DataManager.Instance.gameStateData.ch1MissionClear[0]);
        for (int i = 0; i < DataManager.Instance.gameStateData.ch1MissionClear.Length; i++)
        {
            if (DataManager.Instance.gameStateData.ch1MissionClear[i] == true)
            {
                Debug.Log("clear");
                transform.GetChild(idx).GetComponent<Button>().interactable = true;
            }
            else
            {
                transform.GetChild(idx).GetComponent<Button>().interactable = false;
            }
            idx++;
        }

    }
    public void SetCh2ButtonUnlock(int childStartIndex)
    {
        idx = childStartIndex;
        for (int i = 0; i < DataManager.Instance.gameStateData.ch2MissionClear.Length; i++)
        {
            if (DataManager.Instance.gameStateData.ch2MissionClear[i] == true)
            {
                transform.GetChild(idx).GetComponent<Button>().interactable = true;
            }
            else
            {

                transform.GetChild(idx).GetComponent<Button>().interactable = false;
            }
            idx++;
        }
    }

    public void ResetReviewStage()
    {
        for (int idx = 0; idx < transform.childCount; idx++)
        {
            transform.GetChild(idx).GetComponent<Button>().interactable = false;
        }
    }


    //색깔, interactive
    //눌렀을 때 미션 정보 불러오고 + 씬 이동 처리


}
