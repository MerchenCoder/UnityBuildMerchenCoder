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
}

//로그인 시 전송하는 객체
//pcket 구조체 상속
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
    public TMP_InputField txtId;
    public TMP_InputField txtPassword;
    public GameObject successLoginPanel;
    public GameObject alertPanel;
    public Text alertMessage;

    public Button btnSignin;


    private string id;
    private string password;

    private void Awake()
    {
        successLoginPanel.SetActive(false);
        alertPanel.SetActive(false);
        alertMessage = alertPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }
    void Start()
    {
        // 버튼 이벤트 등록
        btnSignin.onClick.AddListener(() =>
       {
           if (string.IsNullOrEmpty(txtId.text) || string.IsNullOrEmpty(txtPassword.text))
           {
               Debug.Log("ID와 비밀번호를 입력해주세요.");
               alertMessage.text = "ID와 비밀번호를 입력해주세요.";
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
                       successLoginPanel.SetActive(true);
                       Invoke("ChangeSceneToHome", 1.5f);
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

    private void SignIn(System.Action<bool> onComplete)
    {
        req_sign reqSignin = new req_sign();
        reqSignin.cmd = 1100;
        reqSignin.id = id;
        reqSignin.password = password;

        var json = JsonConvert.SerializeObject(reqSignin);

        StartCoroutine(Post("api/signin", json, (result) =>
        {
            // 응답 
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
                Debug.Log("회원등록이 되지 않은 아이디입니다.");
                onComplete(false);
            }
        }));
    }

    private string serverPath = "http://127.0.0.1:3000";

    private IEnumerator Post(string uri, string data, Action<string> onResponse)
    {
        var url = string.Format("{0}/{1}", this.serverPath, uri);
        Debug.Log(url);
        Debug.Log(data);

        var req = new UnityWebRequest(url, "POST");
        byte[] body = Encoding.UTF8.GetBytes(data);
        Debug.Log(body);

        req.uploadHandler = new UploadHandlerRaw(body);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

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


    private void ChangeSceneToHome()
    {
        SceneChange.Instance.ChangetoHome();
    }
}


