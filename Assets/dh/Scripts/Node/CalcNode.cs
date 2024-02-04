using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CalcNode : MonoBehaviour
{
    public int method;
    //0:더하기 1:빼기 2:곱하기 3:나누기 4:나머지

    //outPort
    private GameObject output;

    //outPort operands textmeshpro componenet for updating data
    private TextMeshProUGUI operand1;
    private TextMeshProUGUI operand2;

    //DataInputPort Class
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;
    private GameObject inPort2Text;



    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;


    private int n1;
    private int n2;


    void Start()
    {
        //outPort
        output = transform.GetChild(3).gameObject;
        operand1 = output.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        operand2 = output.transform.GetChild(2).GetComponent<TextMeshProUGUI>();


        //inPort
        inPort2Text = transform.GetChild(2).GetChild(0).gameObject;
        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();
        dataInPort2 = transform.GetChild(2).GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        dataInPort1.StateChanged += HandleStateChanged;
        dataInPort2.StateChanged += HandleStateChanged;

        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "CalcNode";

        //node data
        nodeData = GetComponent<NodeData>();
    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        // if (e.IsConnected)
        // {
        //     if (dataInPort1.IsConnected)
        //     {
        //         /* 
        //         * If dataInPort1 is connected, there are two possible cases. 
        //         * 1. Data with a true error flag: This indicates that the Node parent of the outPort has incorrect data, and its error flag is set to true.
        //         * 2. Data with a false error flag: This represents a normal situation.
        //         * When an outPort is connected to an inPort, it sends its own data along with an error flag. Subsequently, the inPort updates the 'isError' and data variables('inputValueInt', 'inputValueBool', 'inputValueStr')
        //         * Therefore, it is necessary to check whether dataInPort's 'IsError' is true or false.
        //         *

        //         * If it's true -> we need to provide error information to the user and update the output data.
        //         * If it's false -> just update the output data.
        //         * The process of updating inPort data is managed by the inPort itself through DataInPort.cs.
        //         * Therefore, we don't need to concern ourselves with it; we simply update the output data (located next to the outPort arrow).
        //         */


        //         if (dataInPort1.IsError)
        //         {
        //             //error info
        //             // operand1.text = "오류";
        //             // operand1.color = Color.red;
        //         }
        //         else
        //         {
        //             //Take data from inPort using property
        //             n1 = dataInPort1.InputValueInt;
        //             //update outPort operand txt
        //             operand1.text = n1.ToString();
        //             operand1.color = Color.black;
        //         }
        //     }
        //     //same logic for dataInPort2
        //     if (dataInPort2.IsConnected)
        //     {
        //         if (dataInPort2.IsError)
        //         {
        //             operand2.text = "오류";
        //             operand2.color = Color.red;
        //         }
        //         else
        //         {
        //             n2 = dataInPort2.InputValueInt;
        //             operand2.text = n2.ToString();
        //             //update outPort operand txt
        //             operand2.color = Color.black;
        //         }
        //     }
        //     if (dataInPort1.IsConnected && dataInPort2.IsConnected && !dataInPort1.IsError && !dataInPort2.IsError)
        //     {
        //         //when dataInPort1 & 2 are all connected & no error -> process the data(make result)
        //         //Store the result in the 'nodeData' using the 'appropriate property' based on the 'data type' of the result. 
        //         //For instance, if the result is of type 'int,' use the 'SetData_Int' property; if it's of type 'bool,' use the 'SetData_Bool' property.
        //         nodeData.SetData_Int = CalcData(method, n1, n2);
        //     }

        // }
        // else
        // {
        //     //연결 해제인 경우
        //     nodeData.ErrorFlag = true;
        //     if (!dataInPort1.IsConnected)
        //     {
        //         operand1.text = "□";
        //         operand1.color = Color.black;
        //     }
        //     if (!dataInPort2.IsConnected)
        //     {
        //         inPort2Text.GetComponent<TextMeshProUGUI>().color = Color.black;
        //         operand2.text = "△";
        //         operand2.color = Color.black;
        //     }

        // }

    }

    // void HandleOperandColorDivisionByZero(InputPortStateChangedEventArgs e)
    // {
    //     Debug.Log("메시지 받음 in CalcNode");
    //     if ((method == 3 || method == 4) && dataInPort2.InputValueInt <= 0 && dataInPort2.IsConnected)
    //     {
    //         Debug.Log("HandleOperandColorDivisionByZero 조건 만족");
    //         operand2.color = Color.red;
    //         inPort2Text.GetComponent<TextMeshProUGUI>().color = Color.red;
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
                break;
            case 1:
                result = input1 - input2;
                break;
            case 2:
                result = input1 * input2;
                break;
            case 3:
                if (input2 > 0)
                {
                    result = input1 / input2;
                }
                else
                {
                    nodeData.ErrorFlag = true;
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
                    Debug.Log("0 또는 음수로 나눌 수 없습니다.");
                }
                break;
        }
        return result;
    }
}
