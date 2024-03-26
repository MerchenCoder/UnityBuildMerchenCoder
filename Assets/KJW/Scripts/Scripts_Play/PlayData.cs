using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveItem;

public class PlayData : MonoBehaviour
{
    // 씬에서 사용할 알림 Text
    public string nowInfoText;
    public int nowPlayPointIndex;

    // 저장될 폴더 경로
    private string folderPath;
    private string filePath;
    // 초기화용 플레이데이터 폴더 경로
    private string initFolderPath;
    private string initFilePath;


    // JSON 데이터 생성
    [System.Serializable]
    public class SavePlayData
    {
        public List<PlayPoint> playPoints = new List<PlayPoint>();
    }
    [System.Serializable]
    public class PlayPoint
    {
        public string playPointName; // 저장 포인트 이름
        public bool isClear; // 클리어 여부
        public string playInfo; // 저장 포인트에 따른 안내 메시지
    }


    public SavePlayData playData;

    private void Awake()
    {
        // 초기화
        playData = new SavePlayData();

        initFolderPath = "Assets/Data";
        initFilePath = Path.Combine(initFolderPath, "PlayProgressForInit.json");

        folderPath = Application.persistentDataPath + "/Data";
        filePath = Path.Combine(folderPath, "myPlayData.json");

        LoadPlayData();
    }

    private void LoadPlayData()
    {
        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            // 파일이 존재하면 JSON 데이터 읽기
            string jsonData = File.ReadAllText(filePath);

            // JSON 데이터를 역직렬화
            playData = JsonUtility.FromJson<SavePlayData>(jsonData);

            // 현재 진행상황 로드
            for (int i = 0; i < playData.playPoints.Count; i++)
            {
                if (!playData.playPoints[i].isClear)
                {
                    nowPlayPointIndex = i;
                    nowInfoText = playData.playPoints[i].playInfo;
                    break;
                }
            }
        }
        else // 파일이 없다면 초기화된 데이터를 가져와 로컬에 json파일 저장
        {
            // 초기화용 json 파일이 존재하는지 확인
            if (File.Exists(initFilePath))
            {
                // 파일이 존재하면 JSON 데이터 읽기
                string jsonData = File.ReadAllText(initFilePath);

                //폴더 생성
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // 파일에 JSON 데이터 쓰기
                File.WriteAllText(filePath, jsonData);

                // JSON 데이터를 역직렬화
                playData = JsonUtility.FromJson<SavePlayData>(jsonData);

                nowPlayPointIndex = 0;
                nowInfoText = playData.playPoints[0].playPointName;
            }
            else
            {
                Debug.LogError("JSON 파일을 찾을 수 없습니다: " + initFilePath);
            }
        }
    }

    /// <summary>
    /// 플레이 포인트 저장 정보 변경
    /// </summary>
    /// <param name="playPointName"></param>
    /// <param name="isClear"></param>
    public void SavePlayPoint(string playPointName, bool isClear)
    {
        for (int i = 0; i < playData.playPoints.Count; i++)
        {
            if (playData.playPoints[i].playPointName == playPointName)
            {
                playData.playPoints[i].isClear = isClear;
                // JSON 데이터를 문자열로 직렬화
                string jsonData = JsonUtility.ToJson(playData);
                // 파일에 JSON 데이터 쓰기
                File.WriteAllText(filePath, jsonData);
                Debug.Log(playPointName + " 저장 완료");
                LoadPlayData();
                break;
            }
        }
    }

    /// <summary>
    /// 현재 플레이 포인트 확인
    /// </summary>
    /// <param name="playPointName"></param>
    public bool CheckPlayPoint(string playPointName)
    {
        for (int i = 0; i < playData.playPoints.Count; i++)
        {
            if (playData.playPoints[i].playPointName == playPointName)
            {
                if (i == nowPlayPointIndex)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 지나간 플레이 포인트 확인
    /// </summary>
    /// <param name="playPointName"></param>
    public bool CheckClearPlayPoint(string playPointName)
    {
        for (int i = 0; i < playData.playPoints.Count; i++)
        {
            if (playData.playPoints[i].playPointName == playPointName)
            {
                if (playData.playPoints[i].isClear)
                {
                    Debug.Log(playData.playPoints[i].playPointName + " is Clear");
                    return true;
                }
            }
        }
        return false;
    }


    /// <summary>
    /// 디버그용 플레이 포인트 지정하기
    /// </summary>
    /// <param name="playPointName"></param>
    public void SetPlayPoint(string playPointName)
    {
        // 파일이 존재하면 JSON 데이터 읽기
        string jsonData = File.ReadAllText(initFilePath);

        // 파일에 JSON 데이터 쓰기
        File.WriteAllText(filePath, jsonData);

        // JSON 데이터를 역직렬화
        playData = JsonUtility.FromJson<SavePlayData>(jsonData);

        for (int i = 0; i < playData.playPoints.Count; i++)
        {
            if (playData.playPoints[i].playPointName == playPointName)
            {
                playData.playPoints[i].isClear = true;
                break;
            }
            else playData.playPoints[i].isClear = true;
        }

        // JSON 데이터를 문자열로 직렬화
        jsonData = JsonUtility.ToJson(playData);
        // 파일에 JSON 데이터 쓰기
        File.WriteAllText(filePath, jsonData);
        Debug.Log(playPointName + " 저장 완료");
        LoadPlayData();
    }
}
