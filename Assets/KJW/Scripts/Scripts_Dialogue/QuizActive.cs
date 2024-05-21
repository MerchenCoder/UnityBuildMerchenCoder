using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizActive : MonoBehaviour
{
    //플레이어 위치 저장을 위한 변수//
    Button quizBubbleBtn;
    GameObject player;
    GameManager.PlayerData.playerPosition nowPlayerPosition;


    public GameObject quizInfoPanel;

    public string missionCode;
    public bool isDone = false;

    public GameObject AfterMissionDialogue;

    // 문제 맞추고 돌아왔을 때 다음 대화 활성화
    private void Start()
    {
        quizBubbleBtn = GetComponent<Button>();
        player = GameObject.FindWithTag("Player");

        quizBubbleBtn.onClick.RemoveAllListeners();
        quizBubbleBtn.onClick.AddListener(QuizBtnOnClick);

        if (TryGetComponent<PlayProgressControl>(out PlayProgressControl ppc))
        {
            // 현재 플레이 포인트와 일치하는 문제인지 확인
            if (GameManager.Instance.CheckPlayProgress(ppc.activePlayPoint))
            {
                string[] chapter_mission = missionCode.Split("-");
                if(chapter_mission[0] == "1")
                {
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
                else if (chapter_mission[0] == "2")
                {
                    if (DataManager.Instance.gameStateData.ch2MissionClear[int.Parse(chapter_mission[1]) - 1]) // 미션 클리어 확인
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
                else
                {
                    Debug.LogError("Mission Code Error");
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

    private void SavePlayerPosition()
    {
        string thisSceneName = SceneManager.GetActiveScene().name;
        string chapter = thisSceneName.Substring(0, 1);
        GameManager.Instance.playerData.chapterCurrentScene[int.Parse(chapter) - 1] = thisSceneName;

        //포지션 기록

        nowPlayerPosition.x = player.transform.localPosition.x;
        nowPlayerPosition.y = player.transform.localPosition.y;
        nowPlayerPosition.z = player.transform.localPosition.z;
        GameManager.Instance.playerData.playLog[thisSceneName] = nowPlayerPosition;
        GameManager.Instance.SavePlayerData();
    }

    private void SaveCurrentMissionCode()
    {
        int currentChapter = int.Parse(SceneManager.GetActiveScene().name.Substring(0, 1));
        if (currentChapter <= 2 && currentChapter > 0)
        {
            GameManager.Instance.playerData.chapterCurrentMission[currentChapter - 1] = missionCode;
            Debug.Log("현재 미션 코드 저장함");
        }
        else
        {

            Debug.LogWarning("currentChapter 값과 일치하는 챕터 정보가 없습니다.");
        }

    }

    void QuizBtnOnClick()
    {
        QuizActiveTrue();
        SavePlayerPosition();
        SaveCurrentMissionCode();
    }
}
