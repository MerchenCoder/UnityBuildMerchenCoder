using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


//CalcNode.cs 변형한 스크립트. 자세한 설명은 CalcNode.cs의 주석 참고
public class CompNode : MonoBehaviour, INode
{
    public int method;
    //0:같다 1:같지 않다 
    //2:크다 3:크거나 같다
    //4:작다 5:작거나 같다

    //DataInputPort Class
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;

    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;
    private NodeManager nodeManager;


    private int n1;
    private int n2;



    void Start()
    {
        //inPort
        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();
        dataInPort2 = transform.GetChild(2).GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        // dataInPort1.StateChanged += HandleStateChanged;
        // dataInPort2.StateChanged += HandleStateChanged;


        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "CompNode";
        nodeManager = GameObject.FindFirstObjectByType<NodeManager>();


        //node data
        nodeData = GetComponent<NodeData>();
    }

    bool CompData(int method, int input1, int input2)
    {
        bool result = false;
        switch (method)
        {
            case 0:
                result = input1 == input2;
                break;
            case 1:
                result = input1 != input2;
                break;
            case 2:
                result = input1 > input2;
                break;
            case 3:
                result = input1 >= input2;
                break;
            case 4:
                result = input1 < input2;
                break;
            case 5:
                result = input1 <= input2;
                break;
        }
        // When the operation completes successfully, it is necessary to update the errorFlag of the current node itself to false
        nodeData.ErrorFlag = false;
        return result;
    }

    public IEnumerator Execute()
    {
        throw new NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
        {
            Debug.Log("비교연산노드 연결 모두 안됨");
            NodeManager.Instance.SetCompileError(true, "port");
            yield return null;

        }
        else
        {
            yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
            yield return dataInPort2.connectedPort.GetComponent<DataOutPort>().SendData();

            n1 = dataInPort1.InputValueInt;
            n2 = dataInPort2.InputValueInt;
            nodeData.SetData_Bool = CompData(method, n1, n2);
            nodeData.ErrorFlag = false;


            yield return GetComponentInChildren<DataOutPort>().SendData();
            nodeData.ErrorFlag = true;
        }
    }

}
