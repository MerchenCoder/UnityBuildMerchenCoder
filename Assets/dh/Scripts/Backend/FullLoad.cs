using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class FullLoad : MonoBehaviour
{
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
                File.WriteAllText(Path.Combine(folderPath, "GameStatusData.json"), data.gameStatusData);
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
}
