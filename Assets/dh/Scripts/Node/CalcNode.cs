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
    private GameObject outPort;

    //outPort 피연산자 text
    private TextMeshProUGUI operand1;
    private TextMeshProUGUI operand2;

    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;
    private GameObject inPort2Text;
    private DataInPort dataInPort2;



    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;


    void Start()
    {
        //outPort
        outPort = transform.GetChild(3).gameObject;
        operand1 = outPort.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        operand2 = outPort.transform.GetChild(2).GetComponent<TextMeshProUGUI>();


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
                    if ((method == 3 || method == 4) && n2 == 0)
                    {
                        operand2.color = Color.red;
                        inPort2Text.GetComponent<TextMeshProUGUI>().color = Color.red;
                    }
                    else
                    {
                        //update outPort operand txt
                        operand2.color = Color.black;
                        inPort2Text.GetComponent<TextMeshProUGUI>().color = Color.black;


                    }


                }
            }
            if (dataInPort1.IsConnected && dataInPort2.IsConnected && !dataInPort1.IsError && !dataInPort2.IsError)
            {
                //계산
                nodeData.SetData_Int = CalcData(method, n1, n2);
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

                inPort2Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                operand2.text = "△";
                operand2.color = Color.black;
            }

        }

    }


    int CalcData(int method, int input1, int input2)
    {
        Debug.Log("cacData호출");
        nodeData.ErrorFlag = false;
        Debug.Log("호출 후 errorFlag 변경 " + nodeData.ErrorFlag);

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
                try
                {
                    result = input1 / input2;
                }
                catch (DivideByZeroException e)
                {
                    nodeData.ErrorFlag = true;
                    Debug.LogError(e.Message);
                }
                break;
            case 4:
                try
                {
                    result = input1 % input2;
                }
                catch (DivideByZeroException e)
                {
                    nodeData.ErrorFlag = true;
                    Debug.LogError(e.Message);
                }
                break;
        }
        return result;
    }

    // private void FixedUpdate()
    // {
    //     Debug.Log(nodeData.ErrorFlag + "는 calcData의 errorFlag");
    // }


    // public void UpdatePortData(int caseNum)
    // {
    //     //inport 1 연결 = 1
    //     //inport 2 연결 = 2
    //     //outport로 계산결과 출력 = 3
    //     //inport 1 초기화 = 10
    //     //inport 2 초기화 = 20

    //     switch (caseNum)
    //     {
    //         case 1:
    //             input1 = inPort1.GetComponent<DataInPort>().InputValueInt;
    //             input1Val.GetComponent<TextMeshProUGUI>().text = input1.ToString();
    //             outputData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = input1.ToString();
    //             break;
    //         case 2:
    //             input2 = inPort2.GetComponent<DataInPort>().InputValueInt;
    //             if ((method == 3 || method == 4) && input2 == 0)
    //             {
    //                 input2Val.GetComponent<TextMeshProUGUI>().color = Color.red;
    //                 outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.red;
    //             }
    //             else
    //             {
    //                 input2Val.GetComponent<TextMeshProUGUI>().color = Color.black;
    //                 outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;
    //             }
    //             input2Val.GetComponent<TextMeshProUGUI>().text = input2.ToString();
    //             outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = input2.ToString();
    //             break;
    //         case 3:
    //             result = CalcData(method, input1, input2);
    //             Debug.Log("계산결과 : " + result);
    //             this.GetComponent<NodeData>().data_int = result;
    //             break;
    //         case 10:
    //             outputData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "□";
    //             input1Val.GetComponent<TextMeshProUGUI>().text =
    //             this.GetComponent<NodeData>().data_int = 0;
    //             dataErrorManager.ErrorFlag = true;
    //             break;
    //         case 20:
    //             outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
    //             input2Val.GetComponent<TextMeshProUGUI>().text = "△";
    //             this.GetComponent<NodeData>().data_int = 0;
    //             dataErrorManager.ErrorFlag = true;
    //             break;
    //         default:
    //             Debug.Log("연산 노드의 UpdatePortData 함수 호출 과정에서 오류 발생");
    //             dataErrorManager.ErrorFlag = true;
    //             break;
    //     }
    // }



}
