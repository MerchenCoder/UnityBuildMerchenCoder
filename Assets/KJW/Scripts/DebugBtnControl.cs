using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBtnControl : MonoBehaviour
{
    public ReviewButtonControl reviewButtonControl;
    public void GetGemBtn(int num)
    {
        GameManager.Instance.GetSomeGem(num);
    }

    public void SetPlayPointBtn(string point)
    {
        Debug.Log("Debug Button Click : Set Play Point to " + point);
        GameManager.Instance.gameObject.GetComponent<PlayData>().SetPlayPoint(point);
    }

    /// <summary>
    /// 해당 미션 수행 전으로 미션 코드 동기화
    /// </summary>
    /// <param name="nowMissionCode"></param>
    public void SetMissionClearInfoBtn(string nowMissionCode)
    {
        Debug.Log("Debug Button Click : Set Mission Clear Info to " + nowMissionCode);
        DataManager.Instance.InitializeGameStatusData();
        //"-"로 split
        string[] chapter_mission = nowMissionCode.Split("-");
        string[] missionBefore = chapter_mission;
        missionBefore[1] = (int.Parse(chapter_mission[1]) - 1).ToString();

        ResetMissionClearInfo();
        for (int i = 0; i < int.Parse(chapter_mission[0]); i++)
        {
            for (int j = 1; j <= int.Parse(chapter_mission[1]); j++)
            {
                //미션 state clear로 변경
                DataManager.Instance.UpdateMissionState(i + 1, j, true);
                GameManager.Instance.playerData.chapterCurrentMission[i] = missionBefore[0] + "-" + missionBefore[1];
            }
        }
    }

    /// <summary>
    /// 미션 클리어 정보 초기화
    /// </summary>
    private void ResetMissionClearInfo()
    {
        DataManager dataManager = DataManager.Instance;
        for (int i = 1; i <= dataManager.gameStateData.ch1MissionClear.Length; i++)
        {
            DataManager.Instance.UpdateMissionState(1, i, false);
        }
        for (int j = 1; j <= dataManager.gameStateData.ch2MissionClear.Length; j++)
        {
            DataManager.Instance.UpdateMissionState(2, j, false);
        }
    }

    public void SetCurrentScene(string sceneName)
    {
        string[] sceneString = sceneName.Split("_");
        GameManager.Instance.playerData.chapterCurrentScene[int.Parse(sceneString[0]) - 1] = sceneName;
    }

    /// <summary>
    /// 플레이어 포지션 조정
    /// 초기화 (-7,-1.52,0)
    /// </summary>
    /// <param name="x">플레이어 위치 x 좌표</param>
    /// <param name="y">플레이어 위치 y 좌표</param>
    /// <param name="z">플레이어 위치 z 좌표</param>
    /// 
    public void ResetPlayerPosition(float x, float y, float z)
    {
        string[] sceneList = { "1_1_farmer", "1_2_town", "1_3_castle", "1_4_forest" };
        foreach (string scene in sceneList)
        {

            GameManager.PlayerData.playerPosition nowPlayerPosition;
            nowPlayerPosition.x = x;
            nowPlayerPosition.y = y;
            nowPlayerPosition.z = z;
            GameManager.Instance.playerData.playLog[scene] = nowPlayerPosition;

        }
        GameManager.Instance.SavePlayerData();
    }

    /// <summary>
    /// 해리와 만나는 포인트로 변경시 플레이어 마지막 위치 그에 맞게 셋팅
    /// </summary>
    public void SetHarryPlayerPosition()
    {
        GameManager.PlayerData.playerPosition nowPlayerPosition;
        nowPlayerPosition.x = 31.52798f;
        nowPlayerPosition.y = -1.015f;
        nowPlayerPosition.z = 0f;
        GameManager.Instance.playerData.playLog["1_2_town"] = nowPlayerPosition;
        GameManager.Instance.SavePlayerData();

    }

    /// <summary>
    /// 플레이 데이터 (가구 제외) 모두 초기화
    /// </summary>
    public void ResetPlayState()
    {
        Debug.Log("Debug Button Click : Reset");
        GameManager.Instance.gameObject.GetComponent<PlayData>().SetPlayPoint("FirstStart");
        //ResetMissionClearInfo();
        SetCurrentScene("1_1_farmer");
        SetCurrentScene("2_1_Anna");
        ResetPlayerPosition(-7f, -1.52f, 0f);
        GameManager.Instance.playerData.chapterCurrentMission[0] = "";
        GameManager.Instance.playerData.chapterCurrentMission[1] = "";
        SetMissionClearInfoBtn("1-1");

        reviewButtonControl.ResetReviewStage();
    }
}