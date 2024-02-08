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
        data.SetData_Int = intValue;
    }

    public void ValueUpButton()
    {
        if (intValue < 99)
        {
            intValue++;
            UpdateValue();
        }
    }

    public void ValueDownButton()
    {
        if (intValue > -99)
        {
            intValue--;
            UpdateValue();
        }
    }

}
