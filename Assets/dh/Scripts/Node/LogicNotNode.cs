using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LogicNotNode : MonoBehaviour
{

    //outPort
    private GameObject output;

    private TextMeshProUGUI operand1;

    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;


    //node name
    private NodeNameManager nameManager;

    //node data
    private NodeData nodeData;


    void Start()
    {
        output = transform.GetChild(2).gameObject;
        operand1 = output.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        dataInPort1.StateChanged += HandleStateChanged;


        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "LogicNode";


        //node data
        nodeData = GetComponent<NodeData>();


    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        bool b1 = true;
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

                    nodeData.SetData_Bool = !b1;
                    nodeData.ErrorFlag = false;
                }
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
        }

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
