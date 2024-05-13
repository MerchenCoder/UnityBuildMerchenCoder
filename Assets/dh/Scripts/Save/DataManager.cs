using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //게임 데이터 파일 이름 설정
    string GameDataFileName = "GameStatusData.json";

    //저장용 클래스 변수 선언
    public GameData gameStateData = new GameData();


    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Splash")
        {
            LoadGameStatusData();
        }

    }

    //불러오기
    public void LoadGameStatusData()
    {
        //Debug.Log("DataManager LoadGameStatusData");
        // Debug.Log("챕터/미션 상태 데이터 저장 위치 : " + Application.persistentDataPath);
        string filePath = Path.Combine(Application.persistentDataPath, "Data", GameDataFileName); //배포시 사용하는 파일 경로

        if (File.Exists(filePath)) //저장된 게임이 있다면
        {
            //저장된 파일을 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            gameStateData = JsonUtility.FromJson<GameData>(FromJsonData);
            // print("불러오기 완료");
        }
        else
        {
            // 파일이 없을 경우 초기값 설정 후 저장
            Debug.Log("로컬에 저장된 GameStatusData 데이터 없음");
            //InitializeGameStatusData();
        }

    }




    // 초기값 설정
    public void InitializeGameStatusData()
    {
        //초기 데이터 가져오기
        Debug.LogFormat("<color=red>DataManager - 챕터/미션 상태 데이터 초기화</color>");
        SaveGameStatusData();

    }




    //저장하기
    public void SaveGameStatusData()
    {
        string ToJsonData = JsonUtility.ToJson(gameStateData, true);
        string folderPath = Path.Combine(Application.persistentDataPath, "Data");
        string filePath = Path.Combine(folderPath, GameDataFileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(filePath, ToJsonData);

        print("GameStateData.json 저장 완료");

        DataManager.Instance.GetComponent<Save>().SaveGameStatusData();
    }


    //챕터 unlock/lock 변경 메소드
    public void UpdateChapterState(int chapter, bool state)
    {
        gameStateData.chapterIsUnlock[chapter - 1] = state;
        Debug.Log($"chapter{chapter} isUnlock : {state}");

        SaveGameStatusData();

    }

    //mission clear/unclear 변경 메소드
    public void UpdateMissionState(int chapterNum, int missionNum, bool state)
    {
        Debug.Log($"chapter : {chapterNum}, mission : {missionNum}, state:{state} 업데이트");
        if (chapterNum == 1)
        {
            gameStateData.ch1MissionClear[missionNum - 1] = state;

        }
        else if (chapterNum == 2)
        {
            gameStateData.ch2MissionClear[missionNum - 1] = state;
        }
        else
        {
            Debug.Log("There is no chapter data in this game");
        }
        SaveGameStatusData();

    }

    public void ResetMissionState()
    {
        for (int i = 0; i < gameStateData.ch1MissionClear.Length; i++)
        {
            gameStateData.ch1MissionClear[i] = false;
        }
        for (int i = 0; i < gameStateData.ch2MissionClear.Length; i++)
        {
            gameStateData.ch2MissionClear[i] = false;
        }
        SaveGameStatusData();
    }

    /// <summary>
    /// missionNum으로 받은 문제 번호까지 챕터랑 미션 상태 모두 업데이트함.
    /// 1-9부터 플레이하고 싶으면 1-8까지 모두 클리어 상태가 되어야 하므로 missionNum에 8을 넘기면 됨.
    /// 대신 미션 초기화 코드(DataManager.Instance.ResetMissioinState())를 이전에 호출해주어야함
    /// </summary>
    /// <param name="chapterNum"></param>
    /// <param name="missionNum"></param>
    public void Debug_UpdateMissionState(int chapterNum, int missionNum)
    {
        if (chapterNum == 1)
        {
            for (int i = 0; i < missionNum; i++)
            {
                gameStateData.ch1MissionClear[i] = true;
                Debug.LogFormat("<color=green>{0} : {1}</color>", i, true);
            }
            for (int j = missionNum; j < gameStateData.ch1MissionClear.Length; j++)
            {
                gameStateData.ch1MissionClear[j] = false;
                Debug.LogFormat("<color=green>{0} : {1}</color>", j, false);
            }
        }
        else
        {
            for (int i = 0; i < gameStateData.ch1MissionClear.Length; i++)
            {
                gameStateData.ch1MissionClear[i] = true;
            }
            for (int i = 0; i < missionNum; i++)
            {
                gameStateData.ch2MissionClear[i] = true;
            }


        }
        SaveGameStatusData();
    }


    // private void OnApplicationQuit()
    // {
    //     SaveGameStatusData();
    // }





}
