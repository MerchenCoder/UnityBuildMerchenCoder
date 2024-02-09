using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class runNode : MonoBehaviour
{
    public GameObject resultCanvas;
    public GameObject canvas;
    public GameObject[] flowEndPorts; // 배열로 변경
    public GameObject flowStartPort;
    public Button button;

    void Start()
    {
        InitializeFlowEndPorts();
    }

    void Update()
    {
        CheckExecutionAvailability();
    }

    public void Run()
    {
        resultCanvas.SetActive(true);
        NodeManager.Instance.Run();
    }

    private void InitializeFlowEndPorts()
    {
        // canvas 자식들 중 endNode 컴포넌트가 있는 오브젝트를 찾아서 flowEndPorts 배열에 할당
        endNode[] endNodes = canvas.GetComponentsInChildren<endNode>();
        flowEndPorts = new GameObject[endNodes.Length];
        for (int i = 0; i < endNodes.Length; i++)
        {
            flowEndPorts[i] = endNodes[i].gameObject;
        }
    }

    private void CheckExecutionAvailability()
    {
        // flowEndPorts 배열의 모든 요소가 연결되어 있는지 확인
        foreach (GameObject port in flowEndPorts)
        {
            if (!port.GetComponent<endNode>().isConnected)
            {
                Debug.Log("연결 안 됨");
                button.interactable = false;
                break; // 하나라도 연결이 안 되어 있으면 바로 종료
            }
            else
            {
                Debug.Log("연결 됨");
                Debug.Log("버튼 실행 가능");
                button.interactable = true;
            }
        }
        //BtnActive(allEndPortsConnected);
    }
    //public void BtnActive(bool allEndPortsConnected)
    //{
    //    if (allEndPortsConnected && flowStartPort.GetComponent<FlowoutPort>().IsConnected)
    //    {
    //        Debug.Log("버튼 실행 가능");
    //        button.interactable = true;
    //    }
    //    else
    //    {
    //        Debug.Log("버튼 실행 불가능");
    //        button.interactable = false;
    //    }
    //}
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class runNode : MonoBehaviour
//{
//    public GameObject resultCanvas;
//    public GameObject flowEndPort;
//    public GameObject flowStartPort;
//    public Button button;
//    // Start is called before the first frame update
//    void Start()
//    {
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (flowEndPort.GetComponent<endNode>().isConnected == true && flowStartPort.GetComponent<FlowoutPort>().IsConnected == true)  // isConnected 변수 대신 IsConnected 프로퍼티 사용
//        {
//            button.interactable = true;
//            // Debug.Log("실행가능합니다.");
//        }
//        else
//        {
//            button.interactable = false;
//            // Debug.Log("실행 불가능합니다.");
//        }
//    }

//    public void Run()
//    {
//        resultCanvas.SetActive(true);

//        NodeManager.Instance.Run();

//        // if (NodeManager.Instance != null)
//        // {
//        //     NodeManager.Instance.ExecuteNodes();
//        // }
//        // else
//        // {
//        //     Debug.Log("NodeManager.Instance == null");
//        // }
//    }
//}
