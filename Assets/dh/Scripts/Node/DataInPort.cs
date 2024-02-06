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
    // public bool GetDataSignal(get; private set;)

    //생성자로 선언
    // public InputPortStateChangedEventArgs(bool getData)
    // {
    //     GetDataSignal = GetDataSignal;
    // }
}


//상태변화를 감지하고 알리는 클래스
public class DataInPort : MonoBehaviour
{
    //상태변화 이벤트 선언
    public event EventHandler<InputPortStateChangedEventArgs> StateChanged;
    private bool isConnected = false;
    private bool isError = true;
    // private bool getDataSignal = false;


    //가져온 값을 dataInPort에 저장할 때 사용하는 변수
    private int inputValueInt;
    private bool inputValueBool;
    private string inputValueStr;

    [NonSerialized] public DataOutPort connectedPort;


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
            }
        }
    }

    // public bool GetDataSignal
    // {
    //     get
    //     {
    //         return getDataSignal;
    //     }
    //     set
    //     {
    //         if (getDataSignal != value)
    //         {
    //             getDataSignal = value;
    //             OnStateChanged(new InputPortStateChangedEventArgs(getDataSignal));
    //         }
    //     }
    // }

    public int InputValueInt
    {
        get
        {
            return inputValueInt;
        }
        set
        {
            inputValueInt = value;
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
        }
    }



    // // 이벤트 핸들러 메서드를 호출하는 보호된 가상 메서드
    // protected virtual void OnStateChanged(InputPortStateChangedEventArgs e)
    // {
    //     // // 이벤트가 null이 아닌 경우에만 호출
    //     StateChanged?.Invoke(this, e);

    // }


}
