using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CalcNode : MonoBehaviour, INode
{
    public int method;
    //0:더하기 1:빼기 2:곱하기 3:나누기 4:나머지

    //DataInputPort Class
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;



    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;



    private int n1;
    private int n2;


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
        nameManager.NodeName = "CalcNode";

        //node data
        nodeData = GetComponent<NodeData>();
    }
    int CalcData(int method, int input1, int input2)
    {
        nodeData.ErrorFlag = false;
        int result = 0;

        switch (method)
        {
            case 0:
                result = input1 + input2;
                nodeData.ErrorFlag = false;
                break;
            case 1:
                result = input1 - input2;
                nodeData.ErrorFlag = false;
                break;
            case 2:
                result = input1 * input2;
                nodeData.ErrorFlag = false;
                break;
            case 3:
                if (input2 > 0)
                {
                    result = input1 / input2;
                }
                else
                {
                    nodeData.ErrorFlag = true;
                    NodeManager.Instance.SetCompileError(true, "0 또는 음수로 나눌 수 없습니다.\n실행이 중단되었습니다.");
                    Debug.Log("0 또는 음수로 나눌 수 없습니다.");
                }
                break;
            case 4:
                if (input2 > 0)
                {
                    result = input1 % input2;
                }
                else
                {
                    nodeData.ErrorFlag = true;
                    NodeManager.Instance.SetCompileError(true, "0 또는 음수로 나눌 수 없습니다.\n실행이 중단되었습니다.");
                    Debug.Log("0 또는 음수로 나눌 수 없습니다.");
                }
                break;
        }
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
            Debug.Log("산술연산노드 연결 모두 안됨");
            NodeManager.Instance.SetCompileError(true, "port");
            yield return null;
        }
        else
        {
            Debug.Log("계산 시작");
            Debug.Log("값 가져오기");

            yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
            yield return dataInPort2.connectedPort.GetComponent<DataOutPort>().SendData();

            n1 = dataInPort1.InputValueInt;
            n2 = dataInPort2.InputValueInt;
            Debug.Log("값 계산하기");
            nodeData.SetData_Int = CalcData(method, n1, n2);

            yield return GetComponentInChildren<DataOutPort>().SendData();
            nodeData.ErrorFlag = true;
        }



    }
}
