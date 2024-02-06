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

    // void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    // {
    //     if (e.IsConnected)
    //     {
    //         if (dataInPort1.IsConnected)
    //         {
    //             /* 
    //             * If dataInPort1 is connected, there are two possible cases. 
    //             * 1. Data with a true error flag: This indicates that the Node parent of the outPort has incorrect data, and its error flag is set to true.
    //             * 2. Data with a false error flag: This represents a normal situation.
    //             * When an outPort is connected to an inPort, it sends its own data along with an error flag. Subsequently, the inPort updates the 'isError' and data variables('inputValueInt', 'inputValueBool', 'inputValueStr')
    //             * Therefore, it is necessary to check whether dataInPort's 'IsError' is true or false.
    //             *

    //             * If it's true -> we need to provide error information to the user and update the output data.
    //             * If it's false -> just update the output data.
    //             * The process of updating inPort data is managed by the inPort itself through DataInPort.cs.
    //             * Therefore, we don't need to concern ourselves with it; we simply update the output data (located next to the outPort arrow).
    //             */


    //             if (dataInPort1.IsError)
    //             {
    //                 //error info
    //                 // operand1.text = "오류";
    //                 // operand1.color = Color.red;
    //             }
    //             else
    //             {
    //                 //Take data from inPort using property
    //                 n1 = dataInPort1.InputValueInt;
    //                 //update outPort operand txt
    //                 operand1.text = n1.ToString();
    //                 operand1.color = Color.black;
    //             }
    //         }
    //         //same logic for dataInPort2
    //         if (dataInPort2.IsConnected)
    //         {
    //             if (dataInPort2.IsError)
    //             {
    //                 operand2.text = "오류";
    //                 operand2.color = Color.red;
    //             }
    //             else
    //             {
    //                 n2 = dataInPort2.InputValueInt;
    //                 operand2.text = n2.ToString();
    //                 //update outPort operand txt
    //                 operand2.color = Color.black;
    //             }
    //         }
    //         if (dataInPort1.IsConnected && dataInPort2.IsConnected && !dataInPort1.IsError && !dataInPort2.IsError)
    //         {
    //             //when dataInPort1 & 2 are all connected & no error -> process the data(make result)
    //             //Store the result in the 'nodeData' using the 'appropriate property' based on the 'data type' of the result. 
    //             //For instance, if the result is of type 'int,' use the 'SetData_Int' property; if it's of type 'bool,' use the 'SetData_Bool' property.
    //             nodeData.SetData_Int = CalcData(method, n1, n2);
    //         }

    //     }
    //     else
    //     {
    //         //연결 해제인 경우
    //         nodeData.ErrorFlag = true;
    //         if (!dataInPort1.IsConnected)
    //         {
    //             operand1.text = "□";
    //             operand1.color = Color.black;
    //         }
    //         if (!dataInPort2.IsConnected)
    //         {
    //             inPort2Text.GetComponent<TextMeshProUGUI>().color = Color.black;
    //             operand2.text = "△";
    //             operand2.color = Color.black;
    //         }

    //     }

    // }



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
                    NodeManager.Instance.SetCompileError(true);
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
                    NodeManager.Instance.SetCompileError(true);
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
            NodeManager.Instance.SetCompileError(true);
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
        }



    }
}
