using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputPortStateChangedEventArgs : EventArgs
{
    public bool IsConnected { get; private set; }

    //생성자로 선언
    public InputPortStateChangedEventArgs(bool isConnected)
    {
        IsConnected = isConnected;
    }
}


//상태변화를 감지하고 알리는 클래스
public class DataInPort : MonoBehaviour
{
    //상태변화 이벤트 선언
    public event EventHandler<InputPortStateChangedEventArgs> StateChanged;
    private bool isConnected;


    private int inputValueInt;
    private bool inputValueBool;
    private string inputValuStr;


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
                OnStateChanged(new InputPortStateChangedEventArgs(isConnected));

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
            return InputValueStr;
        }
        set
        {
            InputValueStr = value;
        }
    }



    // 이벤트 핸들러 메서드를 호출하는 보호된 가상 메서드
    protected virtual void OnStateChanged(InputPortStateChangedEventArgs e)
    {
        // 이벤트가 null이 아닌 경우에만 호출
        StateChanged?.Invoke(this, e);
        Debug.Log("상태변화 감지 후 연결된 이벤트 핸들러 호출");
    }

}
