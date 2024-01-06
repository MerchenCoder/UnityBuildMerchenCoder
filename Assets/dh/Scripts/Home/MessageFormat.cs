using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MessageFormat : MonoBehaviour
{
    // public int chapterNum;
    public List<string> chapterCodeList;
    string playerName;
    TextMeshProUGUI MissionTitle;
    TextMeshProUGUI Receiver;
    TextMeshProUGUI MessageTxt;

    private void Start()
    {
        playerName = PlayerPrefs.GetString("playerName", "플레이어"); //default "플레이어"
        MissionTitle = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Receiver = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        MessageTxt = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void SetMission(int chaperNum)
    {
        string chapterCode = chapterCodeList[chaperNum - 1];
        string missionTitle;
        //MissionTitle
        switch (chaperNum)
        {
            case 1:
                missionTitle = "첫";
                break;
            case 2:
                missionTitle = "두";
                break;
            case 3:
                missionTitle = "세";
                break;
            default:
                missionTitle = "default";
                break;

        }
        MissionTitle.text = missionTitle + " 번쨰 임무";

        //Receiver
        Receiver.text = "관리자 " + playerName + "에게,";


        //MessageTxt
        MessageTxt.text = "동화 나라 ‘" + chapterCode + "’에서 행복 에너지가 정상적으로 모이지 않고 있다는 보고가 들어왔습니다. 동화 나라에 문제가 생긴 것이 분명합니다. ‘" + chapterCode + "'으로 가서 무슨 일이 일어났는지 확인하고 해결하세요. <color=#1F8CFE>'학습하기' 버튼을 눌러 첫번째 챕터를 모두 해결</color>한 후에 관리국으로 보고를 해주시길 바랍니다.";



    }



}
