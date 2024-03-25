using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizActive : MonoBehaviour
{
    public GameObject quizInfoPanel;

    public string missionCode;
    public bool isDone = false;

    public GameObject AfterMissionDialogue;

    // 문제 맞추고 돌아왔을 때 다음 대화 활성화
    private void Start()
    {
        if(TryGetComponent<PlayProgressControl>(out PlayProgressControl ppc))
        {
            // 현재 플레이 포인트와 일치하는 문제인지 확인
            if (GameManager.Instance.CheckPlayProgress(ppc.activePlayPoint))
            {
                string[] chapter_mission = missionCode.Split("-");
                if (DataManager.Instance.gameStateData.ch1MissionClear[int.Parse(chapter_mission[1]) - 1]) // 미션 클리어 확인
                {
                    Debug.Log("MissionClear");
                    if (AfterMissionDialogue != null)
                    {
                        AfterMissionDialogue.SetActive(true);
                        transform.gameObject.SetActive(false);
                    }
                }
                else // 미션 미클리어
                {
                    if (AfterMissionDialogue != null) AfterMissionDialogue.SetActive(false);
                }
            }
        }
    }

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
