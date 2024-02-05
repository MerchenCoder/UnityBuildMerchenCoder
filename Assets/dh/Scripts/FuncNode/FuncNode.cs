using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuncNode : MonoBehaviour, INode, IFollowFlow
{
    //함수 만들어질때 설정되는 값
    [NonSerialized]
    public int funIndex;
    [NonSerialized]
    public string funName;

    private int type;

    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }


    private DataInPort dataInPort1;
    private DataInPort dataInPort2;

    private GameObject startNode;
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;




    private void Start()
    {

    }

    public IEnumerator Execute()
    {
        Debug.Log("type : " + type.ToString());
        //값 가져오기
        if (type == 3 || type == 4)
        {
            if (type == 3 || type == 4)
            {
                Debug.Log("데이터인포트찾기");
                DataInPort[] dataInPorts = GetComponentsInChildren<DataInPort>();
                if (dataInPorts.Length == 2)
                {
                    dataInPort2 = dataInPorts[1];
                }
                dataInPort1 = dataInPorts[0];
            }

            Debug.Log(dataInPort1.ToString() + " " + dataInPort2.ToString());
            Debug.Log(dataInPort2.IsConnected);

            if ((dataInPort1 != null ? !dataInPort1.IsConnected : false) || (dataInPort2 != null ? !dataInPort2.IsConnected : false))
            {
                Debug.Log("함수 노드의 매개변수가 모두 연결되지 않음");
                NodeManager.Instance.SetCompileError(true);


                yield return null;
            }
            else
            {
                yield return dataInPort1.connectedPort.SendData();
                FunctionManager.Instance.myfuncCanvas[funIndex].GetComponent<ForFunctionRunData>().SetParaValue(dataInPort1, 1);
                if (dataInPort2 != null)
                {
                    yield return dataInPort2.connectedPort.SendData();
                    FunctionManager.Instance.myfuncCanvas[funIndex].GetComponent<ForFunctionRunData>().SetParaValue(dataInPort2, 2);
                }

                Debug.Log("함수 노드에서 연결된 데이터를 받아옴");
            }
        }

        Debug.Log(FunctionManager.Instance.myfuncCanvas[funIndex].name);
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


        }
        currentNode = startNode;
        yield return ExecuteFunction();


    }

    IEnumerator ExecuteFunction()
    {
        Debug.Log("함수 실행 시작");
        while (currentNode.GetComponent<NodeNameManager>().NodeName != "EndNode")
        {
            Debug.Log(currentNode.name);
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
        Debug.Log("Process Data : 함수의 매개변수의 값을 가져오기");


        yield return null;
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}
