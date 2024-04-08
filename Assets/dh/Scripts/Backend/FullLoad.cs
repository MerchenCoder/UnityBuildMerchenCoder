using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
public class req_load : packet
{
}
public class res_load : packet
{
    public int errorno;
    public UserData data;
}
public class UserData
{
    public string playData;
    public string playerData;
    public string itemList;
    public string gameStatusData;
}

public class req_s3Load : packet
{

}
public class res_s3Load : packet
{
    public int errorno;
    public StaticData[] fileData;
}
public class StaticData
{
    public string path;
    public string data;
}

public class FullLoad : MonoBehaviour
{

    /// <summary>
    /// 서버에서 사용자의 아이디로 저장한 모든 json 파일 가져오기
    /// </summary>
    public void LoadAllData(Action<bool> onComplete)
    {
        string folderPath = Application.persistentDataPath;
        string saveFolderPath = Path.Combine(folderPath, "Data");
        LoadFromDB((success, data) =>
        {
            if (success)
            {
                if (data == null)
                {
                    onComplete(true);
                    return;
                }
                if (!Directory.Exists(saveFolderPath))
                {
                    Directory.CreateDirectory(saveFolderPath);
                }
                File.WriteAllText(Path.Combine(saveFolderPath, "GameStatusData.json"), data.gameStatusData);
                File.WriteAllText(Path.Combine(saveFolderPath, "myItemList.json"), data.itemList);
                File.WriteAllText(Path.Combine(saveFolderPath, "myPlayData.json"), data.playData);
                File.WriteAllText(Path.Combine(saveFolderPath, "myPlayerData.json"), data.playerData);

                print("로컬에 덮어쓰기 완료");
                onComplete(true);
            }
            else
            {
                onComplete(false);
            }
        });
    }


    public void LoadFromDB(Action<bool, UserData> onComplete)
    {
        req_load reqLoad = new req_load();
        reqLoad.cmd = 2000;

        var json = JsonConvert.SerializeObject(reqLoad);
        StartCoroutine(NetworkManager.Get($"load/?userId={PlayerPrefs.GetString("userId")}", json, (result) =>
        {
            if (result == "server error")
            {
                Debug.LogError("서버 응답 오류 발생");
                onComplete(false, null);
                return;
            }
            //응답 데이터 역직렬화
            res_load responseResult = JsonConvert.DeserializeObject<res_load>(result);
            Debug.LogFormat("<color=green>{0}</color>", responseResult.cmd);

            if (responseResult.errorno == 0)
            {
                Debug.Log($"데이터베이스에서 데이터 가져오기 성공");
                onComplete(true, responseResult.data);
            }
            else if (responseResult.errorno == 404)
            {
                Debug.Log("조회된 데이터 없음");
                onComplete(true, null);

            }
            else
            {
                Debug.Log(responseResult.errorno);
                onComplete(false, null);
            }
        }));
    }

    public void LoadFromS3(Action<bool> onComplete)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "static");

        req_s3Load reqS3Load = new req_s3Load();
        reqS3Load.cmd = 4000;

        var json = JsonConvert.SerializeObject(reqS3Load);
        StartCoroutine(NetworkManager.Get("download", json, (result) =>
        {
            if (result == "server error")
            {
                Debug.LogError("s3 버킷에서 데이터를 가져오는 중에 오류가 발생했습니다.");
                onComplete(false);
                return;
            }
            //응답 데이터 역직렬화
            res_s3Load responseResult = JsonConvert.DeserializeObject<res_s3Load>(result);
            Debug.LogFormat("<color=purple>{0}</color>", responseResult.cmd);

            if (responseResult.errorno == 0)
            {
                Debug.Log($"데이터베이스에서 데이터 가져오기 성공");

                //폴더 없으면 생성
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Directory.CreateDirectory(Path.Combine(folderPath, "Dialogue"));
                    Directory.CreateDirectory(Path.Combine(folderPath, "MissionInfo"));
                    Directory.CreateDirectory(Path.Combine(folderPath, "TestCase"));
                }
                for (int i = 0; i < responseResult.fileData.Length; i++)
                {
                    File.WriteAllText(Path.Combine(folderPath, responseResult.fileData[i].path), responseResult.fileData[i].data);
                }

                Debug.Log("s3 버킷의 모든 파일을 가져와 로컬에 저장하였습니다.");
                onComplete(true);

            }
            else
            {
                Debug.LogError(responseResult.errorno);
                onComplete(false);
            }

        }));
    }




}
