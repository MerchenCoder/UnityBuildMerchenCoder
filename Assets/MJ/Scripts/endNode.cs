using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node_End : MonoBehaviour
{

    // DataOutPort 클래스의 인스턴스를 저장할 변수
    //public TMDataOutPort dataOutPort;
    public bool isConnected = false;

    private void Start()
    {
    }


    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
        set
        {
            isConnected = value;
        }
    }

    //public void isConnectedEnd()
    //{
    //    if (dataOutPort.IsConnected == true)  // isConnected 변수 대신 IsConnected 프로퍼티 사용
    //    {
    //        isConnected = true;
    //        Debug.Log("끝 노드입니다.");
    //    }
    //    else
    //    {
    //        isConnected = false;
    //    }
    //}
}