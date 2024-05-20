using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntNodeBtn : MonoBehaviour
{
    public TMPro.TMP_InputField ui_text;
    [SerializeField] int intValue;
    NodeData data;

    DataOutPort dataOutPort;

    void Start()
    {
        intValue = 0;
        data = GetComponent<NodeData>();
        dataOutPort = this.transform.GetChild(0).GetComponent<DataOutPort>();
        UpdateValue_ByButton();
    }

    void UpdateValue_ByButton()
    {
        if (intValue >= 9999) intValue = 9999;
        else if (intValue <= -9999) intValue = -9999;
        ui_text.text = intValue.ToString();
        data.SetData_Int = intValue;
    }
    public void UpdateValue_ByInput()
    {
        Debug.Log("데이터 노드 값 변경 함수 호출");
        // if (string.IsNullOrEmpty(ui_text.text))
        // {

        // }
        if (int.TryParse(ui_text.text, out intValue))
        {
            //-9999~9999 범위를 벗어나지 않도록 한다.
            if (intValue < -9999 || intValue > 9999)
            {
                intValue = Mathf.Clamp(intValue, -9999, 9999);
            }

        }
        else
        {
            //숫자가 아닌 문자를 입력한 경우 0으로 변경한다.
            intValue = 0;
        }

        ui_text.text = intValue.ToString();
        data.SetData_Int = intValue;
    }

    public void ValueUpButton()
    {
        if (intValue < 9999)
        {
            intValue++;
            UpdateValue_ByButton();
        }
    }

    public void ValueDownButton()
    {
        if (intValue > -9999)
        {
            intValue--;
            UpdateValue_ByButton();
        }
    }

}
