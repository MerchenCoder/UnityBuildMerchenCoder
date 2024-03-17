using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class runNode : MonoBehaviour
{
    public GameObject canvas;
    public GameObject[] flowEndPorts;
    public GameObject[] inFlows;
    public GameObject flowStartPort;
    public Button button;
    public GameObject[] endNodes;

    void Update()
    {
        InitializeFlowStartPorts();
        InitializeFlowEndPorts(); // Update에서 InitializeFlowEndPorts 호출
        CheckExecutionAvailability();
    }

    private void InitializeFlowStartPorts()
    {
        GameObject[] startNodes = GameObject.FindGameObjectsWithTag("startNode");
        if (startNodes.Length > 0)
        {
            flowStartPort = startNodes[0];
        }
        else
        {
            Debug.Log("No object found with tag 'startNode'");
            flowStartPort = null;
        }
    }

    private void InitializeFlowEndPorts()
    {
        endNodes = GameObject.FindGameObjectsWithTag("endNode");
        if (endNodes.Length > 0)
        {
            flowEndPorts = endNodes;
        }
        else
        {
            Debug.Log("No object found with tag 'endNode'");
            flowEndPorts = null;
        }
    }

    private void CheckExecutionAvailability()
    {
        if (flowEndPorts == null || flowStartPort == null)
        {
            button.interactable = false;
            return;
        }

        // Check if start port is connected
        bool startFlowConnected = false;
        if (flowStartPort != null)
        {
            FlowoutPort outFlow = flowStartPort.GetComponentInChildren<FlowoutPort>();
            if (outFlow != null && outFlow.IsConnected)
            {
                startFlowConnected = true;
            }
        }

        // Check if all end ports are connected
        bool allEndPortsConnected = true; // 기본값을 true로 설정
        GameObject[] inFlows = GameObject.FindGameObjectsWithTag("flow_end");
        foreach (GameObject inflow in inFlows)
        {
            // inflow에 태그가 "flow_end"인 경우
            if (inflow.CompareTag("flow_end"))
            {
                // 해당 오브젝트의 연결 상태를 확인하고 연결되어 있지 않으면 allEndPortsConnected를 false로 설정
                Node_End endNode = inflow.GetComponent<Node_End>();
                if (endNode == null || !endNode.IsConnected)
                {
                    allEndPortsConnected = false;
                    //Debug.Log("End Node not connected: " + inflow.name); // 연결되지 않은 노드 이름을 출력
                }
            }
        }

        // 모든 flowEndPorts가 연결되어 있고 flowStartPort도 연결되어 있다면 실행 가능
        if (allEndPortsConnected && startFlowConnected)
        {
            //Debug.Log("allEndPortsConnected: " + allEndPortsConnected);
            //Debug.Log("startFlowConnected: " + startFlowConnected);
            button.interactable = true;
        }
        else
        {
            //Debug.Log("allEndPortsConnected: " + allEndPortsConnected);
            //Debug.Log("startFlowConnected: " + startFlowConnected);
            button.interactable = false;
        }
    }
}