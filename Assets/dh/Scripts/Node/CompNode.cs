using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


//CalcNode.cs 변형한 스크립트. 자세한 설명은 CalcNode.cs의 주석 참고
public class CompNode : MonoBehaviour
{
    public int method;
    //0:같다 1:같지 않다 
    //2:크다 3:크거나 같다
    //4:작다 5:작거나 같다

    //outPort GameObject
    private GameObject output;

    //outPort operands textmeshpro component for updating data
    private TextMeshProUGUI operand1;
    private TextMeshProUGUI operand2;

    //DataInputPort Class
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;

    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;


    void Start()
    {

        //outPort
        output = transform.GetChild(3).gameObject;
        operand1 = output.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        operand2 = output.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        //inPort
        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();
        dataInPort2 = transform.GetChild(2).GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        dataInPort1.StateChanged += HandleStateChanged;
        dataInPort2.StateChanged += HandleStateChanged;


        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "CompNode";

        //node data
        nodeData = GetComponent<NodeData>();
    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        int n1 = 0;
        int n2 = 0;
        if (e.IsConnected)
        {
            if (dataInPort1.IsConnected)
            {
                if (dataInPort1.IsError)
                {
                    operand1.text = "오류";
                    operand1.color = Color.red;
                }
                else
                {

                    n1 = dataInPort1.InputValueInt;
                    //update outPort operand txt
                    operand1.text = n1.ToString();
                    operand1.color = Color.black;
                }
            }
            if (dataInPort2.IsConnected)
            {
                if (dataInPort2.IsError)
                {
                    operand2.text = "오류";
                    operand2.color = Color.red;
                }
                else
                {
                    n2 = dataInPort2.InputValueInt;
                    operand2.text = n2.ToString();
                }
            }
            if (dataInPort1.IsConnected && dataInPort2.IsConnected && !dataInPort1.IsError && !dataInPort2.IsError)
            {
                nodeData.SetData_Bool = CompData(method, n1, n2);
            }
        }
        else
        {
            //connected가 아닌 경우
            nodeData.ErrorFlag = true;
            if (!dataInPort1.IsConnected)
            {
                operand1.text = "□";
                operand1.color = Color.black;
            }
            if (!dataInPort2.IsConnected)
            {
                operand2.text = "△";
                operand2.color = Color.black;
            }

        }



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
}
