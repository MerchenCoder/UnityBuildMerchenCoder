using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Networking;



public class NetworkManager : MonoBehaviour
{
    private static NetworkManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //server
    //private string serverPath = "http://13.125.154.109";
    public static string serverPath = "http://localhost:3000";


    //post 요청 메소드
    public static IEnumerator Post(string uri, string data, Action<string> onResponse)
    {
        var url = string.Format("{0}/{1}", serverPath, uri);
        // Debug.Log(url);
        // Debug.Log(data);

        var req = new UnityWebRequest(url, "POST");
        byte[] body = Encoding.UTF8.GetBytes(data); //encoding
        req.uploadHandler = new UploadHandlerRaw(body); //웹 요청으로 전송할 데이터 처리. 웹 요청의 본문(body)을 설정하고 웹 서버에 전송할 데이터를 지정
        req.downloadHandler = new DownloadHandlerBuffer(); //수신된 응답 데이터를 처리. 서버에서 받아온 응답 데이터를 처리하고 필요에 따라 저장하거나 분석합니다(단수 버퍼 저장이 대다수)
        req.SetRequestHeader("Content-Type", "application/json"); //header setting

        yield return req.SendWebRequest(); //요청보내고 응답 기다리기

        Debug.Log("Response Code: " + req.responseCode);

        if (req.responseCode == 200)
        {
            // 성공적인 응답을 받았을 때 처리
            onResponse(req.downloadHandler.text);
        }
        else if (req.responseCode >= 400 && req.responseCode < 500)
        {
            // 클라이언트 오류 처리
            onResponse(req.downloadHandler.text);
        }
        else if (req.responseCode >= 500 && req.responseCode < 600)
        {
            // 서버 오류 처리
            onResponse("server error");

        }



    }
}
