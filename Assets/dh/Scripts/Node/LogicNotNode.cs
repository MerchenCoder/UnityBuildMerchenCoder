using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LogicNotNode : MonoBehaviour
{

    //inPort
    private GameObject inPort1;
    private GameObject outputData;

    //operand
    private bool input1;

    //operand gameobject
    private GameObject input1Val;
    //result
    [NonSerialized] public bool result;


    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;


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

        outputData = transform.GetChild(2).gameObject;

        dataInPort1 = inPort1.GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        dataInPort1.StateChanged += HandleStateChanged;

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
                UpdatePortData(3);
            }
        }
        else
        {
            if (!inPort1.GetComponent<DataInPort>().IsConnected)
            {
                UpdatePortData(10);
            }
            else
            {
                UpdatePortData(0000);
            }
        }

    }

    public void UpdatePortData(int caseNum)
    {
        //inport 1 연결 = 1
        //outport로 계산결과 출력 = 3
        //inport 1 초기화 = 10
        switch (caseNum)
        {
            case 1:
                input1 = inPort1.GetComponent<DataInPort>().InputValueBool;
                input1Val.GetComponent<TextMeshProUGUI>().text = BoolToSymbol(input1);
                outputData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = BoolToSymbol(input1);
                break;
            case 3:
                result = !input1;
                Debug.Log("계산결과 : " + result.ToString());
                this.GetComponent<NodeData>().data_bool = result;
                break;
            case 10:
                outputData.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "□";
                input1Val.GetComponent<TextMeshProUGUI>().text = "□";
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
            return "O";
        }
        else
        {
            return "X";
        }
    }

}
