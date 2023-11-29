using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    static GameObject container;


    //---싱글톤 선언----//
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);

            }
            return instance;

        }
    }

    //게임 데이터 파일 이름 설정
    string GameDataFileName = "GameData.json";

    //저장용 클래스 변수 선언
    public Data data = new Data();


    //불러오기
    public void LoadGameData()
    {
        Debug.Log("데이터 저장 위치 : " + Application.dataPath + "/Data/");
        string filePath = Application.dataPath + "/Data/" + GameDataFileName;
        if (File.Exists(filePath)) //저장된 게임이 있다면
        {
            //저장된 파일을 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(FromJsonData);
            print("불러오기 완료");
        }
        else
        {
            // 파일이 없을 경우 초기값 설정 후 저장
            InitializeGameData();
            SaveGameData();
        }

    }


    // 초기값 설정
    private void InitializeGameData()
    {
        // chapterIsUnlock의 첫 인덱스는 true, 나머지는 false
        data.chapterIsUnlock[0] = true;
        for (int i = 1; i < data.chapterIsUnlock.Length; i++)
        {
            data.chapterIsUnlock[i] = false;
        }

        // ch1StageIsUnlock의 첫 인덱스는 true, 나머지는 false
        data.ch1StageIsUnlock[0] = true;
        for (int i = 1; i < data.ch1StageIsUnlock.Length; i++)
        {
            data.ch1StageIsUnlock[i] = false;
        }
    }




    //저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.dataPath + "/Data/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);

        print("저장 완료");
        for (int i = 0; i < data.chapterIsUnlock.Length; i++)
        {
            print($"{i + 1}번 챕터 잠금 해제 여부 : " + data.chapterIsUnlock[i]);
        }
        for (int i = 0; i < data.ch1StageIsUnlock.Length; i++)
        {
            print($"쳅터 1의 {i + 1}번 스테이지 잠금 해제 여부 : " + data.ch1StageIsUnlock[i]);
        }
    }


}
