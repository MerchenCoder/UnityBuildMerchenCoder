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
public class res_login : packet
{
    public int errorno;
    public string user_id;
    public string user_name;
}

//로그인 시 전송하는 객체
//packet 구조체 상속
public class req_login : packet
{
    public string id; //id
    public string password; // password
}

//회원가입 시 응답 객체
public class res_join : packet
{
    public int errorno;
}

//회원가입시 전송하는 객체
public class req_join : packet
{
    public string id;
    public string name;
    public string password;
}

public class Sign : MonoBehaviour
{
    [Header("Sign in")]
    public GameObject loginPanel;

    public InputField login_txtId;
    public InputField login_txtPwd;
    // public GameObject successLoginPanel;
    public GameObject alertPanel;
    private TMP_Text alertMessage;

    public Button btnSignin;

    [Header("Sign up")]
    public GameObject signupPanel;
    public InputField signup_txtId;
    public InputField signup_txtName;
    public InputField signup_txtPwd;

    public Button btnSignup;


    private string id;
    private string password;
    private string name;

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
           if (string.IsNullOrEmpty(login_txtId.text) || string.IsNullOrEmpty(login_txtPwd.text))
           {
               Debug.Log("아이디와 비밀번호를 입력해주세요.");
               alertMessage.text = "아이디와 비밀번호를 입력해주세요.";
               StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
           }
           else
           {
               id = login_txtId.text;
               password = login_txtPwd.text;

               SignIn((success) =>
               {
                   if (success)
                   {
                       loginPanel.SetActive(false);
                       //성공시 gameloading
                       GetComponentInParent<StartGame>().GameLoading();
                   }

               });
           }
       });

        // 버튼 이벤트 등록
        btnSignup.onClick.AddListener(() =>
       {
           //아이디나 비밀번호를 입력하지 않았을 때
           if (string.IsNullOrEmpty(signup_txtId.text) || string.IsNullOrEmpty(signup_txtPwd.text) || string.IsNullOrEmpty(signup_txtName.text))
           {
               Debug.Log("아이디, 이름, 비밀번호를 모두 입력해주세요");
               alertMessage.text = "아이디, 이름, 비밀번호를 모두 입력해주세요";
               StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
           }
           else
           {
               id = signup_txtId.text;
               name = signup_txtName.text;
               password = signup_txtPwd.text;

               SignUp((success) =>
               {
                   if (success)
                   {
                       alertMessage.text = "회원가입이 성공적으로 완료되었습니다.";
                       ResetData();
                       signupPanel.SetActive(false);
                       StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
                   }
               });
           }
       });


    }

    private void SignUp(System.Action<bool> onComplete)
    {
        //패킷 생성
        req_join reqSignup = new req_join();
        reqSignup.cmd = 1200;
        reqSignup.id = id;
        reqSignup.password = password;
        reqSignup.name = name;

        //데이터 직렬화
        var json = JsonConvert.SerializeObject(reqSignup);
        //서버에 POST 요청 보내기
        //요청 완료되면 콜백 함수 'result' 실행
        StartCoroutine(Post("api/signup", json, (result) =>
        {
            //응답 역직렬화
            res_join responseResult = JsonConvert.DeserializeObject<res_join>(result);
            Debug.Log(responseResult);
            Debug.LogFormat("<color=red>{0}</color>", responseResult.cmd);

            //cmd에 따른 처리
            if (responseResult.errorno == 0)
            {
                Debug.Log("회원가입 성공");
                onComplete(true);
            }
            else if (responseResult.errorno == 409)
            {
                Debug.Log("이미 존재하는 계정");
                alertMessage.text = "이미 등록된 아이디입니다. 다른 아이디를 입력해주세요.";
                StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
                onComplete(false);
            }
            else
            {
                Debug.Log(responseResult.errorno);
                alertMessage.text = "클라이언트 오류";
                StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행

                onComplete(false);

            }
        }));
    }

    //요청 보내는 Post 호출 & 응답 처리
    private void SignIn(System.Action<bool> onComplete) //인자로 콜백 함수 'onComplete'를 받음. (delegate)
    {
        //request packet (객체 생성 & 요청에 필요한 데이터 설정)
        req_login reqSignin = new req_login();
        reqSignin.cmd = 1100;
        reqSignin.id = id;
        reqSignin.password = password;

        //json 형식으로 직렬화된 요청 데이터를 생성
        var json = JsonConvert.SerializeObject(reqSignin);

        //서버에 POST 요청 보내기
        //요청 완료되면 콜백 함수 'result' 실행
        StartCoroutine(Post("api/signin", json, (result) =>
        {
            if (result != null)
            {
                // 응답데이터 역직렬화
                res_login responseResult = JsonConvert.DeserializeObject<res_login>(result);
                Debug.Log(responseResult);
                Debug.LogFormat("<color=red>{0}</color>", responseResult.cmd);
                //cmd에 따른 처리
                if (responseResult.errorno == 0)
                {
                    Debug.Log("로그인 성공");
                    onComplete(true);
                }
                else if (responseResult.errorno == 404)
                {

                    Debug.Log("아이디 혹은 비밀번호 오류");
                    alertMessage.text = "아이디 혹은 비밀번호가 일치하지 않습니다.";
                    StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
                    onComplete(false);
                }
                else
                {
                    Debug.Log(responseResult.errorno);
                    alertMessage.text = "클라이언트 오류";
                    StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행

                    onComplete(false);

                }

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
                                           // if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
                                           // {
                                           //     Debug.LogError(req.error);
                                           // }
                                           // else
                                           // {
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

            Debug.LogError("서버 응답 오류가 발생했습니다." + req.responseCode);
            alertMessage.text = "서버 응답 오류가 발생했습니다.";
            StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행

        }
        // }


    }


    private IEnumerator ShowAlertPanel(float delay)
    {
        alertPanel.SetActive(true); // 패널 활성화
        yield return new WaitForSeconds(delay); // 일정 시간 대기
        alertPanel.SetActive(false); // 패널 비활성화
    }


    public void ResetData()
    {
        //input field 초기화
        signup_txtId.text = "";
        signup_txtName.text = "";
        signup_txtPwd.text = "";

        id = "";
        password = "";
        name = "";

    }
}


