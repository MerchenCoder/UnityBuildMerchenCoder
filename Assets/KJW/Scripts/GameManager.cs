using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //싱글톤//
    public static GameManager Instance = null;


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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Splash")
        {
            LoadPlayerData();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Home 씬이 로드되고 플레이어 데이터가 로드되지 않았다면 플레이어 데이터 로드
        if (scene.name == "Home")
        {
            LoadPlayerData();
        }
    }


    //플레이어 데이터 - name, gem, position//


    //json 데이터를 담을 class 선언
    [Serializable]
    public class PlayerData
    {
        public string name;
        public int gem;
        public string[] chapterCurrentScene;
        public Dictionary<string, playerPosition> playLog;
        public struct playerPosition
        {
            public float x;
            public float y;
            public float z;
        }
    }
    public PlayerData playerData = new PlayerData();

    //playerData 파일 이름(초기화용, 사용자 저장용)
    private string playerDataFileName_Init = "PlayerData.json";
    private string playerDataFileName = "myPlayerData.json";


    /// <summary>
    /// playerData 초기화
    /// </summary>
    public void InitializePlayerData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data", playerDataFileName_Init);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);

            //객체 역직렬화 직접 불가 -> 라이브러리 사용
            // playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            playerData = JsonConvert.DeserializeObject<PlayerData>(jsonString);
            Debug.Log(playerDataFileName_Init + " 데이터 불러오기 완료");

            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Data"));
                Debug.Log("폴더 생성");

            }

            SavePlayerData();
        }
        else
        {
            Debug.Log("초기화 게임 파일인 PlayerData.json을 찾을 수 없습니다.");
        }
    }

    //playerData 가져오기
    public void LoadPlayerData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Data", playerDataFileName);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            // playerData = JsonUtility.FromJson<PlayerData>(jsonString);
            playerData = JsonConvert.DeserializeObject<PlayerData>(jsonString);

            // jiwoo add
            PlayerPrefs.SetString("player_name", playerData.name);

            Debug.Log($"{playerDataFileName} 데이터 불러오기 완료");
        }
        else
        {
            Debug.Log("저장된 PlayerData 없음");
            InitializePlayerData();
        }


    }


    //playerData 저장
    public void SavePlayerData()
    {
        // string ToJsonData = JsonUtility.ToJson(playerData, true);
        string ToJsonData = JsonConvert.SerializeObject(playerData);
        string filePath = Path.Combine(Application.persistentDataPath, "Data", playerDataFileName);
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log($"{playerDataFileName} 저장 완료");

        DataManager.Instance.GetComponent<Save>().SavePlayerData();

    }


    // 젬 상태 변화를 알리기 위한 이벤트
    public event UnityAction<int> OnGemChanged;



    /// <summary>
    /// 젬 사용하기
    /// </summary>
    /// <param name="num"></param>
    public bool UseGem(int num)
    {
        if (playerData.gem - num >= 0)
        {
            playerData.gem -= num;
            OnGemChanged?.Invoke(playerData.gem); // 젬 상태가 변경되었음을 알림


            //변경된 잼 정보를 로컬에 저장
            SavePlayerData();
            return true;
        }
        else
        {
            Debug.Log("Gem 부족");
            return false;
        }

    }

    /// <summary>
    /// 현재 젬의 개수 가져오기
    /// </summary>
    /// <returns></returns>
    public int GetNowGem()
    {
        return playerData.gem;
    }

    /// <summary>
    /// 젬 획득하기
    /// </summary>
    /// <param name="num"></param>
    public void GetSomeGem(int num)
    {
        playerData.gem += num;
        OnGemChanged?.Invoke(playerData.gem); // 젬 상태가 변경되었음을 알림

        //변경된 잼 정보를 로컬에 저장
        SavePlayerData();
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
        public bool[] isTabOpenList;

        public bool isClear = false;
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
            Debug.Log("미션 정보 파일을 찾을 수 없습니다.");
        }

    }



    // 플레이 진행상황 정보 저장
    public void SavePlayProgress(string playPointName, bool isClear)
    {
        if (TryGetComponent<PlayData>(out PlayData playData))
        {

            playData.SavePlayPoint(playPointName, isClear);
        }
        else Debug.Log("Play Data is null");
    }


    // 플레이 진행상황 정보 불러오기
    public bool CheckPlayProgress(string playPointName)
    {
        if (TryGetComponent<PlayData>(out PlayData playData))
            return playData.CheckPlayPoint(playPointName);
        else return false;
    }


    public void changeReward(int newReward)
    {
        Debug.Log($"복습하기 모드. 보상 변경 : {newReward}");
        missionData.reward = newReward;
    }
}
