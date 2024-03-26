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
            idx++;
        }
    }


    //색깔, interactive
    //눌렀을 때 미션 정보 불러오고 + 씬 이동 처리


}
