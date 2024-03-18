using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LogicNode : MonoBehaviour, INode
{
    public int method;
    //0:and
    //1:or

    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;



    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;

    bool n1;
    bool n2;


    void Start()
    {
        //inPort
        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();
        dataInPort2 = transform.GetChild(2).GetComponent<DataInPort>();

        // //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        // dataInPort1.StateChanged += HandleStateChanged;
        // dataInPort2.StateChanged += HandleStateChanged;

        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "LogicNode";

        //node data
        nodeData = GetComponent<NodeData>();



    }


    bool LogicData(int method, bool input1, bool input2)
    {

        bool result = false;
        switch (method)
        {
            case 0:
                result = input1 && input2;
                break;
            case 1:
                result = input1 || input2;
                break;
        }
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
            Debug.Log("논리연산노드 연결 모두 안됨");
            NodeManager.Instance.SetCompileError(true, "port");
            yield return null;

        }
        else
        {
            yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
            yield return dataInPort2.connectedPort.GetComponent<DataOutPort>().SendData();

            n1 = dataInPort1.InputValueBool;
            n2 = dataInPort2.InputValueBool;
            nodeData.SetData_Bool = LogicData(method, n1, n2);
            nodeData.ErrorFlag = false;

            yield return GetComponentInChildren<DataOutPort>().SendData();

            nodeData.ErrorFlag = true;
        }
    }

}
