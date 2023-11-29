using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class endNode : MonoBehaviour
{
    // DataOutPort 클래스의 인스턴스를 저장할 변수
    public TMDataOutPort dataOutPort;
    public bool isConnected;

    private void Start()
    {
    }

    public void isConnectedEnd()
    {
        if (dataOutPort.IsConnected == true)  // isConnected 변수 대신 IsConnected 프로퍼티 사용
        {
            Debug.Log("끝 노드입니다.");
        }
    }
}
