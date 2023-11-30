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

    //inPort
    private GameObject inPort1;
    private GameObject inPort2;
    private GameObject outputData;

    //operand
    private int input1;
    private int input2;

    //operand gameobject
    private GameObject input1Val;
    private GameObject input2Val;

    //result
    [NonSerialized] public int result;


    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;

    void Start()
    {

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
            if (inPort1.GetComponent<DataInPort>().IsConnected)
            {
                // inputPort1과 inputPort2가 연결되어 있을 때만 계산 수행
                input1 = inPort1.GetComponent<DataInPort>().InputValue;
                input1Val.GetComponent<TextMeshProUGUI>().text = input1.ToString();
                outputData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = input1.ToString();
            }
            if (inPort2.GetComponent<DataInPort>().IsConnected)
            {
                input2 = inPort2.GetComponent<DataInPort>().InputValue;
                input2Val.GetComponent<TextMeshProUGUI>().text = input2.ToString();
                outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = input2.ToString();
            }
            if (inPort1.GetComponent<DataInPort>().IsConnected && inPort2.GetComponent<DataInPort>().IsConnected)
            {
                result = CalcData(method, input1, input2);
                Debug.Log("계산결과 : " + result);
                this.GetComponent<NodeData>().data_int = result;
            }

        }
        else
        {
            if (!inPort1.GetComponent<DataInPort>().IsConnected)
            {
                outputData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "□";
                input1Val.GetComponent<TextMeshProUGUI>().text = "□";
                this.GetComponent<NodeData>().data_int = 0;
            }
            else if (!inPort2.GetComponent<DataInPort>().IsConnected)
            {
                outputData.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "△";
                input2Val.GetComponent<TextMeshProUGUI>().text = "△";
                this.GetComponent<NodeData>().data_int = 0;
            }
            else
            {
                Debug.Log("오류 예외상황");
            }
        }

    }


    int CalcData(int method, int input1, int input2)
    {
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
                result = input1 / input2;
                break;
            case 4:
                result = input1 % input2;
                break;
        }
        return result;
    }
}
