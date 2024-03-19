using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int Gem_Num;

    // 젬 상태 변화를 알리기 위한 이벤트
    public event UnityAction<int> OnGemChanged;

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

    /// <summary>
    /// 젬 사용하기
    /// </summary>
    /// <param name="num"></param>
    public bool UseGem(int num)
    {
        if (Gem_Num - num >= 0)
        {
            Gem_Num -= num;
            OnGemChanged?.Invoke(Gem_Num); // 젬 상태가 변경되었음을 알림
            return true;
        }
        else return false;

    }

    /// <summary>
    /// 현재 젬의 개수 가져오기
    /// </summary>
    /// <returns></returns>
    public int GetNowGem()
    {
        return Gem_Num;
    }

    /// <summary>
    /// 젬 획득하기
    /// </summary>
    /// <param name="num"></param>
    public void GetSomeGem(int num)
    {
        Gem_Num += num;
        OnGemChanged?.Invoke(Gem_Num); // 젬 상태가 변경되었음을 알림
    }




    //미션 정보 가져오는 부분을 위한 변수 선언 및 메서드
    [Serializable]
    public class MissionData
    {
        public string missionCode;
        public string missionTitle;
        public string[] actionNodes;
        public string missionInfo;
        public string[] nodeLabels;
        public int reward;
        public bool hasNodeLimit;
        public int nodeOpenIndex;
    }
    public string dataFileName;



    public MissionData missionData = new MissionData();

    public void LoadMissionData(string missionCode)
    {
        dataFileName = "Mission" + missionCode + ".json";
        string filePath = Application.dataPath + "/Data/MissionInfo/" + dataFileName;
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            missionData = JsonUtility.FromJson<MissionData>(jsonString);

            Debug.Log(dataFileName + " 데이터 불러오기 완료");
        }
        else
        {
            Debug.Log("can't find file");
        }

    }



    // 플레이 진행상황 정보 저장
    public void SavePlayProgress(string playPointName, bool isClear)
    {
        if (TryGetComponent<PlayData>(out PlayData playData))
            playData.SavePlayPoint(playPointName, isClear);
        else Debug.Log("Play Data is null");
    }


    // 플레이 진행상황 정보 불러오기
    public bool CheckPlayProgress(string playPointName)
    {
        if (TryGetComponent<PlayData>(out PlayData playData))
            return playData.CheckPlayPoint(playPointName);
        else return false;
    }
}
