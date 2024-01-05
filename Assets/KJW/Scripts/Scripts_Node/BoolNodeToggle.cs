using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolNodeToggle : MonoBehaviour
{
    bool boolValue = true;
    NodeData data;

    DataOutPort dataOutPort;

    void Start()
    {
        boolValue = true;
        data = GetComponent<NodeData>();
        dataOutPort = this.transform.GetChild(0).GetComponent<DataOutPort>();
        UpdateValue();
    }

    void UpdateValue()
    {
        data.data_bool = boolValue;

        if (dataOutPort.isConnected)
        {
            //연결된 calcNode 찾기
            dataOutPort.connectedPort.GetComponent<DataInPort>().InputValueBool = boolValue;
            dataOutPort.connectedPort.GetComponent<DataInPort>().IsConnected = false;
            dataOutPort.connectedPort.GetComponent<DataInPort>().IsConnected = true;
        }

    }

    public void ValueSetTrue()
    {
        boolValue = true;
        UpdateValue();
    }

    public void ValueSetFalse()
    {
        boolValue = false;
        UpdateValue();
    }
}
