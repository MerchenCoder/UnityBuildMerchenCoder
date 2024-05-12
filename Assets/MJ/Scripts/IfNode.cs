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
    GameObject nextOutportObject;
    private bool trueConntected;
    private bool falseConnected;
    private bool nextConnected;

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
        nextOutportObject = transform.Find("outFlow").gameObject;
    }

    IEnumerator INode.Execute()
    {
        trueConntected = trueOutportObject.GetComponent<FlowoutPort>().IsConnected;
        falseConnected = falseOutportObject.GetComponent<FlowoutPort>().IsConnected;
        nextConnected = nextOutportObject.GetComponent<FlowoutPort>().IsConnected;

        if (!nextConnected)
        {
            //Debug.Log("====!nextConnected=====");
            Debug.Log("초록색 OutFlow는 연결되어야 합니다!");
            NodeManager.Instance.SetCompileError(true, "flow");
            yield break;
        }
        else if (!trueConntected && !falseConnected)
        {
            //Debug.Log("====!trueConntected || !falseConnected=====");
            Debug.Log("Outport 중 하나는 연결되어야 합니다!");
            NodeManager.Instance.SetCompileError(true, "flow");
            yield break;
        }
        else
        {
            //Debug.Log("====trueConntected && falseConnected=====");
            if (!dataInPort1.IsConnected)
            {
                Debug.Log("Data 노드 연결 안됨");
                NodeManager.Instance.SetCompileError(true, "flow");
                yield return null;
            }
            else
            {
                Debug.Log("Data 노드 연결 됨");
                yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
                n1 = dataInPort1.InputValueBool;
                if (n1 == true)
                {
                    Debug.Log("conclusion: n1 is true");
                    nodeData.ErrorFlag = false;
                    yield return this.transform.Find("outputTrueFlow").GetComponent<FlowoutPort>();
                }
                else
                {
                    Debug.Log("conclusion: n1 is false");
                    nodeData.ErrorFlag = false;
                    yield return this.transform.Find("outputFalseFlow").GetComponent<FlowoutPort>();
                }
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
        //Debug.Log("This is NextFlow");
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}