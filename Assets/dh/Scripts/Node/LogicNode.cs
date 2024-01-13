using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LogicNode : MonoBehaviour
{
    public int method;
    //0:and
    //1:or

    private GameObject output;

    private TextMeshProUGUI operand1;
    private TextMeshProUGUI operand2;

    //DataInputPort 클래스 참조
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
        nameManager.NodeName = "LogicNode";

        //node data
        nodeData = GetComponent<NodeData>();

    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        bool b1 = true;
        bool b2 = true;

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
                    b1 = dataInPort1.InputValueBool;
                    operand1.text = b1.ToString();
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
                    b2 = dataInPort2.InputValueBool;
                    operand2.text = b2.ToString();
                    operand2.color = Color.black;
                }
            }
            if (dataInPort1.IsConnected && dataInPort2.IsConnected && !dataInPort1.IsError && !dataInPort2.IsError)
            {
                nodeData.SetData_Bool = LogicData(method, b1, b2);
            }

        }
        else
        {
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
    public string BoolToSymbol(bool value)
    {
        if (value)
        {
            return "참";
        }
        else
        {
            return "거짓";
        }
    }

}
