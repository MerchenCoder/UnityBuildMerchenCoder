using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuncNode : MonoBehaviour, INode
{
    //함수 만들어질때 설정되는 값
    [NonSerialized]
    public int funIndex;
    [NonSerialized]
    public string funName;
    [NonSerialized]
    public int type;

    private DataInPort dataInPort1;
    private DataInPort dataInPort2;

    private GameObject startNode;
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;




    private void Start()
    {
        if (type == 3 || type == 4)
        {
            DataInPort[] dataInPorts = GetComponentsInChildren<DataInPort>();
            if (dataInPorts.Length == 2)
            {
                dataInPort2 = dataInPorts[1];
            }
            dataInPort1 = dataInPorts[0];
        }
    }


    public IEnumerator Execute()
    {
        //값 가져오기
        if (type == 3 || type == 4)
        {
            if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
            {
                Debug.Log("함수 노드의 매개변수가 모두 연결되지 않음");
                NodeManager.Instance.SetCompileError(true);


                yield return null;
            }
            else
            {
                yield return dataInPort1.connectedPort.SendData();
                yield return dataInPort2.connectedPort.SendData();

                FunctionManager.Instance.myfuncCanvas[funIndex].GetComponent<ForFunctionRunData>().SetParaValue(dataInPort1, 1);
                FunctionManager.Instance.myfuncCanvas[funIndex].GetComponent<ForFunctionRunData>().SetParaValue(dataInPort2, 2);

            }
        }

        NodeNameManager[] nodes = FunctionManager.Instance.myfuncCanvas[funIndex].transform.GetChild(0).GetComponentsInChildren<NodeNameManager>();
        foreach (NodeNameManager node in nodes)
        {
            if (node.NodeName == "StartNode")
            {
                startNode = node.gameObject;
                break;
            }
            Debug.Log("start 노드를 찾을 수 없습니다.");
            yield return null;

            currentNode = startNode;

            StartCoroutine(ExecuteFunction());
        }


    }

    IEnumerator ExecuteFunction()
    {
        while (currentNode.GetComponent<NodeNameManager>().NodeName != "EndNode")
        {
            Debug.Log("함수 실행 시작");
            yield return currentNode.GetComponent<INode>().Execute();

            currentFlowoutPort = currentNode.GetComponent<IFollowFlow>().NextFlow();
            currentNode = NextNode(currentFlowoutPort);
        }

        //endnode에서 처리할 일
        //....

        Debug.Log("함수 실행 종료");
        yield return null;
    }


    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        return flowoutPort.ConnectedPort.transform.parent.gameObject;
    }

    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }

}
