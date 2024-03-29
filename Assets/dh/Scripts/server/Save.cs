using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;


public class Save : MonoBehaviour
{
    public class req_save : packet
    {
        public string tableName;
        public string jsonData;
    }
    public class res_save : packet
    {
        public int errorno;
    }

    private string tableName;
    private string jsonData;
    private string filePath;
    private string folderPath;

    private void Start()
    {
        folderPath = Application.persistentDataPath;
    }


    /// <summary>
    /// myPlayData.json을 db에 저장하는 요청을 서버에 보낸댜.
    /// </summary>
    public void SavePlayData()
    {
        tableName = "play_data_table";
        filePath = Path.Combine(folderPath, "Data", "myPlayData.json");
        jsonData = File.ReadAllText(filePath);
        SaveToDB((success) =>
{
    if (success)
    {
        Debug.Log("myPlayData.json 서버에 저장 성공");
    }
    else
    {
        Debug.LogError("myPlayData.json 서버에 저장 실패");
    }
});

    }


    /// <summary>
    /// myPlayerData.json을 db에 저장하는 요청을 서버에 보낸댜.
    /// </summary>
    public void SavePlayerData()
    {
        tableName = "player_data_table";
        filePath = Path.Combine(folderPath, "Data", "myPlayerData.json");
        jsonData = File.ReadAllText(filePath);
        SaveToDB((success) =>
        {
            if (success)
            {
                Debug.Log("myPlayerData.json 서버에 저장 성공");
            }
            else
            {
                Debug.LogError("myPlayerData.json 서버에 저장 실패");
            }
        });

    }
    /// <summary>
    /// myItemList.json을 db에 저장하는 요청을 서버에 보낸댜.
    /// </summary>
    public void SaveItemList()
    {
        tableName = "item_list_table";
        filePath = Path.Combine(folderPath, "Data", "myItemList.json");
        jsonData = File.ReadAllText(filePath);
        SaveToDB((success) =>
{
    if (success)
    {
        Debug.Log("myItemList.json 서버에 저장 성공");
    }
    else
    {
        Debug.LogError("myItemList.json 서버에 저장 실패");
    }
});
    }

    /// <summary>
    /// GameStatusData.json을 db에 저장하는 요청
    /// </summary>
    public void SaveGameStatusData()
    {
        tableName = "game_status_data_table";
        filePath = Path.Combine(folderPath, "GameStatusData.json");
        jsonData = File.ReadAllText(filePath);
        SaveToDB((success) =>
{
    if (success)
    {
        Debug.Log("GameStatusData.json 서버에 저장 성공");
    }
    else
    {
        Debug.LogError("GameStatusData.json 서버에 저장 실패");
    }
});
    }
    /// <summary>
    /// 전체 저장 요청
    /// </summary>
    public void SaveAll()
    {
        SaveGameStatusData();
        SaveItemList();
        SavePlayData();
        SavePlayerData();

    }

    //서버로 save 요청 보내는 메소드
    public void SaveToDB(System.Action<bool> onComplete)
    {
        req_save reqSave = new req_save();
        reqSave.cmd = 3000;
        reqSave.tableName = tableName;
        reqSave.jsonData = jsonData;

        var json = JsonConvert.SerializeObject(reqSave);
        StartCoroutine(NetworkManager.Post($"save/{PlayerPrefs.GetString("userId")}", json, (result) =>
        {
            if (result == "server error")
            {
                Debug.LogError("서버 응답 오류가 발생했습니다.");
                return;
            }
            //응답 역직렬화
            // Debug.Log("서버 응답: " + result);
            res_save responseResult = JsonConvert.DeserializeObject<res_save>(result);
            //Debug.Log(responseResult);
            Debug.LogFormat("<color=blue>{0}</color>", responseResult.cmd);

            //cmd에 따른 처리
            if (responseResult.errorno == 0)
            {
                Debug.Log($"데이터베이스에 데이터 저장 성공");
                onComplete(true);
            }
            else
            {
                Debug.Log(responseResult.errorno);
                onComplete(false);

            }
        }));






    }

}
