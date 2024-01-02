using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntNodeBtn : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ui_text;
    int intValue;
    NodeData data;

    DataOutPort dataOutPort;

    void Start()
    {
        intValue = 0;
        data = GetComponent<NodeData>();
        dataOutPort = this.transform.GetChild(0).GetComponent<DataOutPort>();
        UpdateValue();
    }

    void UpdateValue()
    {
        ui_text.text = intValue.ToString();
        data.data_int = intValue;



        if (dataOutPort.IsConnected)
        {
            //연결된 calcNode 찾기
            // GameObject connectedCalcNode = dataOutPort.ConnectedPort.transform.parent.GetComponent<CalcNode>().UpdatePortData()
            dataOutPort.ConnectedPort.GetComponent<DataInPort>().InputValue = intValue;
            dataOutPort.ConnectedPort.GetComponent<DataInPort>().IsConnected = false;
            dataOutPort.ConnectedPort.GetComponent<DataInPort>().IsConnected = true;
        }


    }

    public void ValueUpButton()
    {
        intValue++;
        UpdateValue();
    }

    public void ValueDownButton()
    {
        intValue--;
        UpdateValue();
    }

}
