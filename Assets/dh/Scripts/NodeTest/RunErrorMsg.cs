using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunErrorMsg : MonoBehaviour
{
    public GameObject ErrorMsgBox;
    public TextMeshProUGUI State;

    // Start is called before the first frame update




    public void SetStateRun()
    {
        State.text = "실행 중";
        State.color = Color.black;

    }

    public void SetStateStop()
    {
        State.text = "실행 중단";
        State.color = Color.red;
    }

    public void SetStateComplete()
    {
        State.text = "실행 완료";
        State.color = Color.blue;
    }


    public void ActiveErrorMsg(string error)
    {
        string errorMsg = null;
        if (error == "flow")
        {
            errorMsg = "플로우 연결에 문제가 있습니다.\n실행이 중단되었습니다.";
        }
        else if (error == "port")
        {
            errorMsg = "연결되지 않은 데이터 포트가 있습니다.\n실행이 중단되었습니다.";
        }
        else if (error == "startNode")
        {
            errorMsg = "시작 노드를 찾을 수 없습니다.\n실행이 중단되었습니다.";

        }
        else if (error == "value")
        {
            errorMsg = "값 설정하기/가져오기 노드의\n변수를 설정하지 않았습니다. \n실행이 중단되었습니다.";
        }
        else
        {
            errorMsg = error;
        }
        ErrorMsgBox.GetComponentInChildren<TextMeshProUGUI>(true).text = errorMsg;
        ErrorMsgBox.SetActive(true);

    }
    public void InActiveErrorMsg()
    {
        ErrorMsgBox.SetActive(false);
    }

    void Start()
    {
        SetStateRun();
        ErrorMsgBox = transform.parent.GetChild(1).GetChild(0).gameObject;
        ErrorMsgBox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
