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
    public Toggle autoLoginToggle;

    public Button btnSignin;

    [Header("Sign up")]
    public GameObject signupPanel;
    public InputField signup_txtId;
    public InputField signup_txtName;
    public InputField signup_txtPwd;

    public Button btnSignup;


    private string id;
    private string password;
    private string uname;

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

               Login(login_txtId.text, login_txtPwd.text);
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
               uname = signup_txtName.text;
               password = signup_txtPwd.text;

               SignUp((success) =>
               {
                   if (success)
                   {
                       alertMessage.text = "회원가입이 성공적으로 완료되었습니다.";
                       PlayerPrefs.DeleteAll();

                       //로컬 라이브러리 > Data 폴더 내 json 파일 다 삭제(플레이 기록 관련 파일)
                       if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Data")))
                       {
                           string[] files = Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Data"), "*.*", SearchOption.AllDirectories);
                           // 모든 파일 삭제
                           foreach (string filePath in files)
                           {
                               File.Delete(filePath);
                               Debug.Log("로컬에 존재하는 json파일 삭제");
                           }

                       }
                       else
                       {
                           Debug.Log("폴더가 존재하지 않습니다.");
                       }

                       ResetData();
                       signupPanel.SetActive(false);
                       signupPanel.transform.parent.gameObject.SetActive(false);
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
        reqSignup.name = uname;

        //데이터 직렬화
        var json = JsonConvert.SerializeObject(reqSignup);
        //서버에 POST 요청 보내기
        //요청 완료되면 콜백 함수 'result' 실행
        StartCoroutine(NetworkManager.Post("user/signup", json, (result) =>
        {
            if (result == "server error")
            {
                Debug.LogError("서버 응답 오류가 발생했습니다.");
                alertMessage.text = "서버 응답 오류가 발생했습니다.";
                StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
                return;
            }
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
        StartCoroutine(NetworkManager.Post("user/signin", json, (result) =>
        {
            if (result != null)
            {
                if (result == "server error")
                {
                    Debug.LogError("서버 응답 오류가 발생했습니다.");
                    alertMessage.text = "서버 응답 오류가 발생했습니다.";
                    StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
                    return;
                }
                // 응답데이터 역직렬화
                res_login responseResult = JsonConvert.DeserializeObject<res_login>(result);
                Debug.LogFormat("<color=red>{0}</color>", responseResult.cmd);
                //cmd에 따른 처리
                if (responseResult.errorno == 0)
                {
                    Debug.Log("로그인 성공");
                    onComplete(true);
                }
                else if (responseResult.errorno == 404)
                {

                    Debug.Log("아이디 존재하지 않음");
                    alertMessage.text = "아이디가 존재하지 않습니다. 다시 확인해주세요.";
                    StartCoroutine(ShowAlertPanel(1.5f)); // ShowAlertPanel 코루틴 실행
                    onComplete(false);
                }
                else if (responseResult.errorno == 401)
                {
                    Debug.Log("비밀번호 오류");
                    alertMessage.text = "비밀번호가 일치하지 않습니다.";
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
        login_txtId.text = "";
        login_txtPwd.text = "";

        id = "";
        password = "";
        uname = "";

    }



    public void Logout()
    {
        PlayerPrefs.DeleteAll();
    }


    public void Login(string uid, string upwd)
    {
        id = uid;
        password = upwd;
        SignIn((success) =>
               {
                   if (success)
                   {

                       if (autoLoginToggle.isOn)
                       {
                           if (!PlayerPrefs.HasKey("autoLogin") || PlayerPrefs.GetInt("autoLogin") == 0)
                           {
                               PlayerPrefs.SetInt("autoLogin", 1);
                               PlayerPrefs.SetString("userId", id);
                               PlayerPrefs.SetString("userPwd", password);
                               Debug.Log("data save for auto login");
                           }
                           //로그인 한 id만 저장
                           PlayerPrefs.SetString("userId", id);
                       }
                       else
                       {
                           //로그인 한 id만 저장
                           PlayerPrefs.SetString("userId", id);
                       }

                       Debug.Log($"현재 사용자 : {PlayerPrefs.GetString("userId")}");

                       loginPanel.SetActive(false);
                       loginPanel.transform.parent.gameObject.SetActive(false);
                       //성공시 gameloading
                       GetComponentInParent<StartGame>().GameLoading();
                   }

               });
    }

}


