using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class IfNode : MonoBehaviour, INode, IFollowFlow
{
    // node name
    private NodeNameManager nameManager;

    // nodeData
    private NodeData nodeData;
    private bool n1;

    GameObject trueOutportObject;
    GameObject falseOutportObject;
    private bool trueConntected;
    private bool falseConnected;

    // DataInputPort 클래스 참조
    private DataInPort dataInPort1;

    void Start()
    {
        // inPort
        dataInPort1 = transform.GetChild(4).GetComponent<DataInPort>(); //data 받아옴

        // node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "IfNode";

        // node data
        nodeData = GetComponent<NodeData>();

        trueOutportObject = transform.Find("outputTrueFlow").gameObject;
        falseOutportObject = transform.Find("outputFalseFlow").gameObject;
    }

    IEnumerator INode.Execute()
    {
        trueConntected = trueOutportObject.GetComponent<FlowoutPort>().IsConnected;
        falseConnected = falseOutportObject.GetComponent<FlowoutPort>().IsConnected;
        if (!trueConntected || !falseConnected)
        {
            Debug.Log("Outport는 모두 연결되어야 합니다!");
            NodeManager.Instance.SetCompileError(true);

            yield return null;
        }
        else
        {
            if (!dataInPort1.IsConnected)
            {
                Debug.Log("Data 노드 연결 안됨");
                NodeManager.Instance.SetCompileError(true);
                yield return null;
            }
            else
            {
                Debug.Log("Data 노드 연결 됨");
                yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
                NextFlow();
            }
            GetComponent<NodeData>().ErrorFlag = false;
        }
    }
    public IEnumerator ProcessData()
    {
        yield return null;
    }

    public FlowoutPort NextFlow()
    {
        n1 = dataInPort1.InputValueBool;
        if (n1 == true)
        {
            Debug.Log("conclusion: n1 is true");
            nodeData.ErrorFlag = false;
            return this.transform.Find("outputTrueFlow").GetComponent<FlowoutPort>(); ;
        }
        else
        {
            Debug.Log("conclusion: n1 is false");
            nodeData.ErrorFlag = false;
            return this.transform.Find("outputFalseFlow").GetComponent<FlowoutPort>(); ;
        }
    }
}