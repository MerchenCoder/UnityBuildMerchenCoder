using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UIElements;

public class ValueNodeDropdown : MonoBehaviour
{
    //public AddValueBtn valueBtn;
    private TMPro.TMP_Dropdown tMP_Dropdown;
    private ValueManager valueManager;

    // null option
    TMP_Dropdown.OptionData optionData_null = new TMP_Dropdown.OptionData();

    void Start()
    {
        tMP_Dropdown = GetComponent<TMPro.TMP_Dropdown>();
        //valueBtn = GameObject.Find("AddValueBtn").gameObject.GetComponent<AddValueBtn>();
        valueManager = GameObject.Find("ValueManager").gameObject.GetComponent<ValueManager>();

        valueManager.OnUpdateValues += AddOption;

        optionData_null.text = "선택되지 않음";
        CallValueList();
    }

    private void CallValueList()
    {
        //tMP_Dropdown.options.Add(optionData_null);
        if (CompareTag("data_int")) 
        {
            for(int i=0; i<valueManager.intValues.Count; i++)
            {
                TMP_Dropdown.OptionData optionData_temp = new TMP_Dropdown.OptionData();
                optionData_temp.text = valueManager.intValues[i].valueName;
                tMP_Dropdown.options.Add(optionData_temp);
            }
        }
        else if (CompareTag("data_bool"))
        {
            for (int i = 0; i < valueManager.boolValues.Count; i++)
            {
                TMP_Dropdown.OptionData optionData_temp = new TMP_Dropdown.OptionData();
                optionData_temp.text = valueManager.boolValues[i].valueName;
                tMP_Dropdown.options.Add(optionData_temp);
            }
        }
        else if (CompareTag("data_string"))
        {
            for (int i = 0; i < valueManager.stringValues.Count; i++)
            {
                TMP_Dropdown.OptionData optionData_temp = new TMP_Dropdown.OptionData();
                optionData_temp.text = valueManager.stringValues[i].valueName;
                tMP_Dropdown.options.Add(optionData_temp);
            }
        }
    }

    private void AddOption(object sender, EventArgs e)
    {
        TMP_Dropdown.OptionData optionData_temp = new TMP_Dropdown.OptionData();

        Debug.Log(valueManager.intValues.Count + "," + valueManager.boolValues.Count + "," + valueManager.stringValues.Count);

        if (CompareTag("data_int") && valueManager.lastAddedType == 0)
        {
            optionData_temp.text = valueManager.intValues[valueManager.intValues.Count - 1].valueName;
            tMP_Dropdown.options.Add(optionData_temp);
        }
        else if (CompareTag("data_bool") && valueManager.lastAddedType == 1)
        {
            optionData_temp.text = valueManager.boolValues[valueManager.boolValues.Count - 1].valueName;
            tMP_Dropdown.options.Add(optionData_temp);
        }
        else if (CompareTag("data_string") && valueManager.lastAddedType == 2)
        {
            optionData_temp.text = valueManager.stringValues[valueManager.stringValues.Count - 1].valueName;
            tMP_Dropdown.options.Add(optionData_temp);
        }
    }

}
