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

        if (dataOutPort.isConnected)
        {
            //연결된 calcNode 찾기
            // GameObject connectedCalcNode = dataOutPort.ConnectedPort.transform.parent.GetComponent<CalcNode>().UpdatePortData()
            dataOutPort.connectedPort.GetComponent<DataInPort>().InputValueInt = intValue;
            dataOutPort.connectedPort.GetComponent<DataInPort>().IsConnected = false;
            dataOutPort.connectedPort.GetComponent<DataInPort>().IsConnected = true;
        }


    }

    public void ValueUpButton()
    {
        if(intValue < 99)
        {
            intValue++;
            UpdateValue();
        }
    }

    public void ValueDownButton()
    {
        if(intValue > -99)
        {
            intValue--;
            UpdateValue();
        }
    }

}
