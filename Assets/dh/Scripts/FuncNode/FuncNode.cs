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

    private NodeData nodeData;




    private void Start()
    {
        nodeData = GetComponent<NodeData>();
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
            else
            {
                Debug.Log("start 노드를 찾을 수 없습니다.");
                NodeManager.Instance.SetCompileError(true);
                Debug.Log("FunNode Excute() 종료.");
                yield break;
            }



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
            if (currentNode == null)
            {
                Debug.Log("FuncNode의 ExcuteFunction 코루틴 종료");
                yield break;
            }
        }

        //반환값이 있다면 가져와야함
        if (type == 2 || type == 4)
        {
            Debug.Log("반환값 가져와서 함수 노드에 저장하기");
            GameObject outPort = transform.GetChild(0).gameObject;
            ForFunctionRunData forFunctionRunData = FunctionManager.Instance.myfuncCanvas[funIndex].GetComponent<ForFunctionRunData>();
            if (outPort.tag == "data_int")
            {
                nodeData.SetData_Int = forFunctionRunData.rt_int;
                Debug.Log("반환값은 : " + nodeData.data_int.ToString());
            }
            else if (outPort.tag == "data_bool")
            {
                nodeData.SetData_Bool = forFunctionRunData.rt_bool;
                Debug.Log("반환값은 : " + nodeData.data_bool.ToString());
            }
            else
            {
                nodeData.SetData_string = forFunctionRunData.rt_string;
                Debug.Log("반환값은 : " + nodeData.data_string);
            }
            nodeData.ErrorFlag = false;
        }
        Debug.Log(funName + " 함수 실행 종료");
        yield return null;
    }


    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        if (flowoutPort.transform.GetComponentInParent<NodeNameManager>(true).NodeName == "ReturnNode")
        {
            return flowoutPort.transform.parent.GetChild(0).gameObject;

        }
        else
        {
            if (flowoutPort.isConnected)
            {
                return flowoutPort.ConnectedPort.transform.parent.gameObject;
            }
            else
            {
                Debug.Log("flow 문제 발생한 노드는 : " + flowoutPort.transform.parent.name);
                Debug.Log("Flow 연결에 문제가 있습니다.");
                NodeManager.Instance.SetCompileError(true);
                return null;
            }

        }
    }



    public IEnumerator ProcessData()
    {
        Debug.Log("Process Data : 함수의 매개변수의 값을 가져오기");
        Debug.Log("먼저 함수를 실행해서 반환값을 불러와 노드에 저장");
        yield return Execute();

        Debug.Log("다음 포트로 값 전달하기");
        yield return GetComponentInChildren<DataOutPort>().SendData();
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}
