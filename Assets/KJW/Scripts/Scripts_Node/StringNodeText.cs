using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StringNodeText : MonoBehaviour
{
    public TMP_InputField inputField;

    string stringValue = null;
    NodeData data;
    DataOutPort dataOutPort;

    void Start()
    {
        stringValue = null;
        data = GetComponent<NodeData>();
        dataOutPort = this.transform.GetChild(0).GetComponent<DataOutPort>();
        UpdateValue();
        if (inputField == null)
        {
            inputField = this.transform.GetChild(1).GetComponent<TMP_InputField>();
        }
    }

    void UpdateValue()
    {
        data.data_string = stringValue;
        if (dataOutPort.isConnected)
        {
            dataOutPort.connectedPort.GetComponent<DataInPort>().InputValueStr = stringValue;
            dataOutPort.connectedPort.GetComponent<DataInPort>().IsConnected = false;
            dataOutPort.connectedPort.GetComponent<DataInPort>().IsConnected = true;
        }

        // 튜토리얼 플래그 추가 240513
        if (FlagManager.instance != null)
        {
            if (FlagManager.instance.flagStr == "PrintHello")
            {
                if (stringValue == "안녕하세요")
                    FlagManager.instance.OffFlag();
            }
            if (FlagManager.instance.flagStr == "PrintMango")
            {
                if (stringValue == "망고망고")
                    FlagManager.instance.OffFlag();
            }
        }
    }

    public void ValueSet()
    {
        if (inputField != null)
        {
            stringValue = inputField.text;
            UpdateValue();
        }
        else Debug.Log("InpuField 지정 안됨");
    }
}
