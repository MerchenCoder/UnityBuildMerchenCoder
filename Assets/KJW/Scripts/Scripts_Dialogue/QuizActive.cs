using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizActive : MonoBehaviour
{
    public GameObject quizInfoPanel;

    public string missionCode;
    public bool isDone = false;

    public void QuizActiveTrue()
    {
        if (!isDone)
        {
            //미션 정보 가져오기
            GameManager.Instance.LoadMissionData(missionCode);
            //가져온 미션 정보로 mission panel 내부 셋팅하기

            quizInfoPanel.GetComponent<MissionManager>().MissionInfoSetting();
            isDone = true; //한 번 셋팅하고 나면 다시 셋팅 안해도 되도록 처리
        }

        quizInfoPanel.SetActive(true);
    }

}
