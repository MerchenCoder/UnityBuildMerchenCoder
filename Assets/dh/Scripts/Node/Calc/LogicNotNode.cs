using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LogicNotNode : MonoBehaviour, INode
{

    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;


    //node name
    private NodeNameManager nameManager;

    //node data
    private NodeData nodeData;

    private bool n1;


    void Start()
    {
        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        // dataInPort1.StateChanged += HandleStateChanged;


        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "LogicNode";



        //node data
        nodeData = GetComponent<NodeData>();


    }
    public IEnumerator Execute()
    {
        throw new NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected)
        {
            Debug.Log("논리연산노드 not 연결 모두 안됨");
            NodeManager.Instance.SetCompileError(true, "port");
            yield return null;

        }
        else
        {
            yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();

            n1 = dataInPort1.InputValueBool;
            nodeData.SetData_Bool = !n1;
            nodeData.ErrorFlag = false;

            yield return GetComponentInChildren<DataOutPort>().SendData();
            nodeData.ErrorFlag = true;

        }
    }

}
