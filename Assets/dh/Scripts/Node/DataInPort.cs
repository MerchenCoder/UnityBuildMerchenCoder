using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class InputPortStateChangedEventArgs : EventArgs
{
    public bool IsConnected { get; private set; }
    public bool IsError { get; private set; }

    //생성자로 선언
    public InputPortStateChangedEventArgs(bool isConnected, bool isError)
    {
        IsConnected = isConnected;
        IsError = isError;
    }
}


//상태변화를 감지하고 알리는 클래스
public class DataInPort : MonoBehaviour
{
    //상태변화 이벤트 선언
    public event EventHandler<InputPortStateChangedEventArgs> StateChanged;
    private bool isConnected = false;
    private bool isError = true;


    private int inputValueInt;
    private bool inputValueBool;
    private string inputValueStr;

    [NonSerialized] public DataOutPort connectedPort;

    //inPort data text
    private TextMeshProUGUI inPortText;
    private string originTextData;


    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
        set
        {
            if (isConnected != value) //현재 연결 상태 업데이트
            {
                isConnected = value;
                //상태 변화 이벤트 발생
                OnStateChanged(new InputPortStateChangedEventArgs(isConnected, isError));

            }
        }
    }

    public bool IsError
    {
        get
        {
            return isError;
        }
        set
        {
            if (isError != value)
            {
                isError = value;
                OnStateChanged(new InputPortStateChangedEventArgs(isConnected, isError));
            }
        }
    }

    public int InputValueInt
    {
        get
        {
            return inputValueInt;
        }
        set
        {
            inputValueInt = value;
            OnStateChanged(new InputPortStateChangedEventArgs(isConnected, isError));
        }
    }

    public bool InputValueBool
    {
        get
        {
            return inputValueBool;
        }
        set
        {
            inputValueBool = value;
            OnStateChanged(new InputPortStateChangedEventArgs(isConnected, isError));
        }
    }
    public string InputValueStr
    {
        get
        {
            return inputValueStr;
        }
        set
        {
            inputValueStr = value;
            OnStateChanged(new InputPortStateChangedEventArgs(isConnected, isError));
        }
    }



    // 이벤트 핸들러 메서드를 호출하는 보호된 가상 메서드
    protected virtual void OnStateChanged(InputPortStateChangedEventArgs e)
    {
        // // 이벤트가 null이 아닌 경우에만 호출
        StateChanged?.Invoke(this, e);
        // if (e.IsConnected)
        // {
        //     if (e.IsError)
        //     {
        //         inPortText.text = "오류";
        //         inPortText.color = Color.red;
        //     }
        //     else
        //     {
        //         inPortText.color = Color.black;
        //         if (this.CompareTag("data_int"))
        //         {
        //             UpdatePortData(0);
        //             if (transform.parent.GetComponent<NodeNameManager>().NodeName == "CalcNode")
        //             {
        //                 Debug.Log("메시지 보내기");

        //                 transform.parent.SendMessage("HandleOperandColorDivisionByZero", e);
        //             }
        //         }
        //         else if (this.CompareTag("data_bool"))
        //         {
        //             UpdatePortData(1);
        //         }
        //         else if (this.CompareTag("data_string"))
        //         {
        //             UpdatePortData(2);
        //         }
        //         else
        //         {
        //             //data_all
        //             UpdatePortData(3);
        //         }

        //     }
        // }
        // else
        // {
        //     inPortText.color = Color.black;
        //     UpdatePortData(-1);
        // }

    }


    private void Start()
    {
        // inPortText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        // originTextData = inPortText.text;
    }





    public void UpdatePortData(int dataType)
    {
        //int = 0
        //bool = 1
        //string = 2
        switch (dataType)
        {
            case 0:
                inPortText.text = inputValueInt.ToString();
                break;
            case 1:
                inPortText.text = inputValueBool.ToString().Substring(0, 1);
                break;
            case 2:
                inPortText.text = inputValueStr;
                break;
            case -1:
                inPortText.text = originTextData;
                break;

        }

    }

}
