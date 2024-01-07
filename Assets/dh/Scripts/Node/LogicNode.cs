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

    //inPort
    private GameObject inPort1;
    private GameObject inPort2;
    private GameObject outputData;

    //operand
    private bool input1;
    private bool input2;

    //operand gameobject
    private GameObject input1Val;
    private GameObject input2Val;

    //result
    [NonSerialized] public bool result;


    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;



    //node name
    private NodeNameManager nameManager;

    //flag
    private bool errorFlag = false;
    public bool ErrorFlag
    {
        get
        {
            return errorFlag;
        }
    }


    void Start()
    {
        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "LogicNode";


        inPort1 = transform.GetChild(1).gameObject;
        input1Val = inPort1.transform.GetChild(0).gameObject;

        inPort2 = transform.GetChild(2).gameObject;
        input2Val = inPort2.transform.GetChild(0).gameObject;

        outputData = transform.GetChild(3).gameObject;

        dataInPort1 = inPort1.GetComponent<DataInPort>();
        dataInPort2 = inPort2.GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        dataInPort1.StateChanged += HandleStateChanged;
        dataInPort2.StateChanged += HandleStateChanged;

    }

    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        //inputPort에 연결된 StateChanged 이벤트에서 isConnected가 바뀌면 이벤트가 발생
        //inputPort는 두개 있음
        //두 포트의 isConnected가 true로 바뀔때만 계산함.
        //가져온 값을 각각 input1, input2로 할당하고 CalcData 호출 예정
        if (e.IsConnected)
        {
            // inputPort1과 inputPort2가 연결되어 있을 때만 계산 수행
            if (inPort1.GetComponent<DataInPort>().IsConnected)
            {
                Debug.Log("update Port 1");
                UpdatePortData(1);
            }
            if (inPort2.GetComponent<DataInPort>().IsConnected)
            {
                Debug.Log("update Port 2");
                UpdatePortData(2);
            }
            if (inPort1.GetComponent<DataInPort>().IsConnected && inPort2.GetComponent<DataInPort>().IsConnected)
            {
                UpdatePortData(3);
            }

        }
        else
        {
            if (!inPort1.GetComponent<DataInPort>().IsConnected)
            {
                UpdatePortData(10);
            }
            else if (!inPort2.GetComponent<DataInPort>().IsConnected)
            {
                UpdatePortData(20);
            }
            else
            {
                UpdatePortData(0000);
            }
        }

    }


    bool CalcData(int method, bool input1, bool input2)
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
        return result;
    }


    public void UpdatePortData(int caseNum)
    {
        //inport 1 연결 = 1
        //inport 2 연결 = 2
        //outport로 계산결과 출력 = 3
        //inport 1 초기화 = 10
        //inport 2 초기화 = 20

        switch (caseNum)
        {
            case 1:
                input1 = inPort1.GetComponent<DataInPort>().InputValueBool;
                input1Val.GetComponent<TextMeshProUGUI>().text = BoolToSymbol(input1);
                outputData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = BoolToSymbol(input1);
                break;
            case 2:
                input2 = inPort2.GetComponent<DataInPort>().InputValueBool;
                input2Val.GetComponent<TextMeshProUGUI>().text = BoolToSymbol(input2);
                outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = BoolToSymbol(input2);
                break;
            case 3:
                result = CalcData(method, input1, input2);
                Debug.Log("계산결과 : " + result.ToString());
                this.GetComponent<NodeData>().data_bool = result;
                break;
            case 10:
                outputData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "□";
                input1Val.GetComponent<TextMeshProUGUI>().text = "□";
                this.GetComponent<NodeData>().data_bool = false;
                break;
            case 20:
                outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "△";
                input2Val.GetComponent<TextMeshProUGUI>().text = "△";
                this.GetComponent<NodeData>().data_bool = false;
                break;
            default:
                Debug.Log("연산 노드의 UpdatePortData 함수 호출 과정에서 오류 발생");
                break;
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
