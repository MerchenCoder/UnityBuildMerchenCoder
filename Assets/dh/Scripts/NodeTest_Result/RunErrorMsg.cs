using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunErrorMsg : MonoBehaviour
{

    public GameObject ErrorMsgBox;
    public TextMeshProUGUI State;

    [SerializeField] private RefreshCanvas refreshCanvas;


    public void SetStateRun()
    {
        State.text = "실행 중";
        State.color = Color.black;

    }

    public void SetStateStop()
    {
        if (NodeManager.Instance.Mode == "run")
            refreshCanvas.PlayRunErrorSound();
        State.text = "실행 중단";
        State.color = Color.red;
    }

    public void SetStateComplete()
    {
        if (NodeManager.Instance.Mode == "run")
            refreshCanvas.PlayRunCompleteSound();
        State.text = "실행 완료";
        State.color = Color.blue;

        if (FlagManager.instance != null)
        {
            if (FlagManager.instance.flagStr == "RunEnd")
            {
                FlagManager.instance.OffFlag();
            }
        }
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
        else if (error == "data type")
        {
            errorMsg = "자료형이 다른 데이터 간에\n연산을 수행할 수 없습니다. \n실행이 중단되었습니다.";
        }
        else
        {
            errorMsg = error;
        }
        if (ErrorMsgBox == null)
        {
            ErrorMsgBox = transform.parent.GetChild(1).GetChild(0).gameObject;
        }
        ErrorMsgBox.GetComponentInChildren<TextMeshProUGUI>(true).text = errorMsg;
        ErrorMsgBox.SetActive(true);
        if (NodeManager.Instance.Mode == "submit")
        {
            Invoke("DisableOnSubmitMode", 1.5f);
        }

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
        refreshCanvas = GetComponent<RefreshCanvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void DisableOnSubmitMode()
    {
        ErrorMsgBox.SetActive(false);
        TestManager.Instance.Fail();


    }
}
