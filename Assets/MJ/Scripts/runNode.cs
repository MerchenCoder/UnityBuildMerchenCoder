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
    public GameObject[] inFlows; // inFlow 객체 배열
    public GameObject flowStartPort;
    public Button button;

    void Start()
    {
        InitializeInFlows();
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

    private void InitializeInFlows()
    {
        List<GameObject> inFlowObjects = new List<GameObject>();

        // canvas의 모든 하위 객체를 검사하여 이름이 "inFlow"인 것을 찾음
        foreach (Transform child in canvas.GetComponentsInChildren<Transform>())
        {
            if (child.name == "inFlow")
            {
                inFlowObjects.Add(child.gameObject);
            }
        }

        // List를 배열로 변환
        inFlows = inFlowObjects.ToArray();
        Debug.Log("inFlows Length = " + inFlows.Length);
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
        bool allEndPortsConnected = true;
        // inFlows 배열의 모든 요소가 연결되어 있는지 확인
        foreach (GameObject port in flowEndPorts)
        {
            // 포트가 파괴되었는지 확인
            if (port == null)
            {
                // 포트가 파괴되었으면 반복문을 종료하고 함수를 빠져나감
                allEndPortsConnected = false;
                break;
            }

            // 연결 상태 확인
            if (!port.GetComponent<endNode>().isConnected)
            {
                allEndPortsConnected = false;
                break; // 하나라도 연결이 안 되어 있으면 바로 종료
            }
        }

        // 모든 inFlows가 연결되어 있는지 확인
        bool allInFlowsConnected = true;
        foreach (GameObject inFlowObject in inFlows)
        {
            FlowinPort inFlowPort = inFlowObject.GetComponent<FlowinPort>();
            if (inFlowPort == null || !inFlowPort.IsConnected)
            {
                allInFlowsConnected = false;
                break;
            }
        }

        // 모든 flowEndPorts가 연결되어 있고 flowStartPort도 연결되어 있다면 실행 가능
        if (allEndPortsConnected && (flowStartPort != null) && allInFlowsConnected && (flowStartPort.GetComponent<FlowoutPort>() != null))
        {
            button.interactable = flowStartPort.GetComponent<FlowoutPort>().IsConnected;
        }
        else
        {
            button.interactable = false;
        }
    }
}