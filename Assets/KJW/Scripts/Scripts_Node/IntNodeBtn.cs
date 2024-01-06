using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntNodeBtn : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ui_text;
    int intValue;
    NodeData data;

    void Start()
    {
        intValue = 0;
        data = GetComponent<NodeData>();
        UpdateValue();
    }

    void UpdateValue()
    {
        ui_text.text = intValue.ToString();
        data.data_int = intValue;
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
