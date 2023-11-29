using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runNode : MonoBehaviour
{
    public GameObject flowEndPort;
    public GameObject flowStartPort;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (flowEndPort.GetComponent<endNode>().isConnected == true &&flowStartPort.GetComponent<TMDataOutPort>().IsConnected == true)  // isConnected 변수 대신 IsConnected 프로퍼티 사용
        {
            button.interactable = true;
            Debug.Log("실행가능합니다.");
        }
        else
        {
            button.interactable = false;
            Debug.Log("실행 불가능합니다.");
        }
    }

    //public void isConnectedNodeRun()
    //{
    //    if (dataOutPort.IsConnected == true)  // isConnected 변수 대신 IsConnected 프로퍼티 사용
    //    {
    //        button.interactable = true;
    //        Debug.Log("실행가능합니다.");
    //    }
    //    else
    //    {
    //        button.interactable = false;
    //        Debug.Log("실행 불가능합니다.");
    //    }
    //}

}
