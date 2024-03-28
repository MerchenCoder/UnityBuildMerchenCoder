using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

#region 패킷 클래스 정의
public class packet
{
    public int cmd;
}
#endregion

//로그인 시 응답 객체
//packet 구조체 상속
public class res_sign : packet
{
    public int errorno;
    public string user_id;
    public string user_name;
}

//로그인 시 전송하는 객체
//packet 구조체 상속
public class req_sign : packet
{
    public string id; //id
    public string password; // password
}

//회원가입 시 응답 객체
// public class res_join : packet
// {
//     public int errorno;
// }

// //회원가입시 전송하는 객체
// public class req_join : packet
// {
//     public string uid;
//     public string nickname;
// }

public class Login : MonoBehaviour
{
    public GameObject loginPanel;
    public InputField txtId;
    public InputField txtPassword;
    // public GameObject successLoginPanel;
    public GameObject alertPanel;
    private TMP_Text alertMessage;

    public Button btnSignin;


    private string id;
    private string password;

    private void Awake()
    {
        alertPanel.SetActive(false);
        alertMessage = alertPanel.GetComponentInChildren<TMP_Text>(true);
    }
    void Start()
    {
        // 버튼 이벤트 등록
        btnSignin.onClick.AddListener(() =>
       {
           //아이디나 비밀번호를 입력하지 않았을 때
           if (string.IsNullOrEmpty(txtId.text) || string.IsNullOrEmpty(txtPassword.text))
           {
               Debug.Log("아이디와 비밀번호를 입력해주세요.");
               alertMessage.text = "아이디와 비밀번호를 입력해주세요.";
               alertOn(1.5f);
           }
           else
           {
               id = txtId.text;
               password = txtPassword.text;

               // 추가적인 ID와 Password 입력 처리
               Debug.Log("ID: " + id + ", Password: " + password);

               SignIn((success) =>
               {
                   if (success)
                   {
                       loginPanel.SetActive(false);
                       //성공시 gameloading
                       GetComponentInParent<StartGame>().GameLoading();
                   }
                   else
                   {
                       Debug.Log("아이디 혹은 비밀번호가 일치하지 않습니다.");
                       alertMessage.text = "아이디 혹은 비밀번호가 일치하지 않습니다.";
                       alertOn(1.5f);
                   }
               });
           }
       });
    }

    //요청 보내는 Post 호출 & 응답 처리
    private void SignIn(System.Action<bool> onComplete) //인자로 콜백 함수 'onComplete'를 받음. (delegate)
    {
        //request packet (객체 생성 & 요청에 필요한 데이터 설정)
        req_sign reqSignin = new req_sign();
        reqSignin.cmd = 1100;
        reqSignin.id = id;
        reqSignin.password = password;

        //json 형식으로 직렬화된 요청 데이터를 생성
        var json = JsonConvert.SerializeObject(reqSignin);

        //서버에 POST 요청 보내기
        //요청 완료되면 콜백 함수 'result' 실행
        StartCoroutine(Post("api/signin", json, (result) =>
        {
            // 응답데이터 역직렬화
            res_sign responseResult = JsonConvert.DeserializeObject<res_sign>(result);
            Debug.Log(responseResult);
            Debug.LogFormat("<color=red>{0}</color>", responseResult.cmd);

            if (responseResult.cmd == 200)
            {
                Debug.Log("로그인 성공");
                onComplete(true);
            }
            else if (responseResult.errorno == 9001)
            {
                Debug.Log("아이디 혹은 비밀번호 오류");
                onComplete(false);
            }
            else
            {
                Debug.Log("서버/데이터베이스/쿼리 오류");
                alertMessage.text = "로그인 중에 오류가 발생했습니다.\n나중에 다시 시도해 주세요.";
                alertOn(1.5f);
            }
        }));
    }



    //server
    //private string serverPath = "http://13.125.154.109";
    private string serverPath = "http://localhost:3000";



    //post 요청 메소드
    private IEnumerator Post(string uri, string data, Action<string> onResponse)
    {
        var url = string.Format("{0}/{1}", this.serverPath, uri);
        Debug.Log(url);
        Debug.Log(data);

        var req = new UnityWebRequest(url, "POST");
        byte[] body = Encoding.UTF8.GetBytes(data); //encoding
        req.uploadHandler = new UploadHandlerRaw(body); //웹 요청으로 전송할 데이터 처리. 웹 요청의 본문(body)을 설정하고 웹 서버에 전송할 데이터를 지정
        req.downloadHandler = new DownloadHandlerBuffer(); //수신된 응답 데이터를 처리. 서버에서 받아온 응답 데이터를 처리하고 필요에 따라 저장하거나 분석합니다(단수 버퍼 저장이 대다수)
        req.SetRequestHeader("Content-Type", "application/json"); //header setting

        yield return req.SendWebRequest(); //요청보내고 응답 기다리기

        onResponse(req.downloadHandler.text);
    }




    private void alertOn(float second)
    {
        alertPanel.SetActive(true);
        Invoke("alertOff", second);
    }
    private void alertOff()
    {
        alertPanel.SetActive(false);
    }
}


