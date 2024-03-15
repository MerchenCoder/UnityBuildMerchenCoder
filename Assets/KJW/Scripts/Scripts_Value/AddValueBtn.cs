using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AddValueBtn : MonoBehaviour
{
    private ValueManager valueManager;
    private NodeData nodeData;
    public TMP_Dropdown dropdown;
    private GameObject valueNameSettingUI;
    private TMPro.TMP_InputField inputField;
    // public GameObject valuePrefab;
    // private GameObject addedValue;

    private void Start()
    {
        valueManager = GameObject.Find("ValueManager").GetComponent<ValueManager>();
        valueNameSettingUI = GetComponentInParent<Canvas>().transform.Find("ValueUI").transform.GetChild(0).gameObject;
        Debug.Log(valueNameSettingUI);
        inputField = valueNameSettingUI.transform.GetChild(1).GetComponent<TMP_InputField>();
        valueNameSettingUI.SetActive(false);
        dropdown = GetComponent<TMP_Dropdown>();
        nodeData = transform.parent.GetComponent<NodeData>();
    }

    public void OnChangeValue()
    {
        if (dropdown.value == 0)
        {
            // null select
            // if connected with node in this state -> ??? 
            nodeData.ErrorFlag = true;
        }
        else if (dropdown.value == 1 && transform.parent.GetComponent<NodeNameManager>().NodeName == "SetValueNode")
        {
            // add value
            SetValueName();
            dropdown.SetValueWithoutNotify(0);
            nodeData.ErrorFlag = true;
        }
        else if (transform.parent.GetComponent<NodeNameManager>().NodeName == "SetValueNode")
        {
            // select exist value
            if (CompareTag("data_int"))
            {
                nodeData.data_int = valueManager.intValues[dropdown.value - 2].valueOfValue;
            }
            else if (CompareTag("data_bool"))
            {
                nodeData.data_bool = valueManager.boolValues[dropdown.value - 2].valueOfValue;
            }
            else if (CompareTag("data_string"))
            {
                nodeData.data_string = valueManager.stringValues[dropdown.value - 2].valueOfValue;
            }
        }
        else
        {
            // select exist value
            if (CompareTag("data_int"))
            {
                nodeData.data_int = valueManager.intValues[dropdown.value - 1].valueOfValue;
            }
            else if (CompareTag("data_bool"))
            {
                nodeData.data_bool = valueManager.boolValues[dropdown.value - 1].valueOfValue;
            }
            else if (CompareTag("data_string"))
            {
                nodeData.data_string = valueManager.stringValues[dropdown.value - 1].valueOfValue;
            }
        }
    }


    public void SetValueName()
    {
        inputField.text = null;
        valueNameSettingUI.SetActive(true);
        valueNameSettingUI.tag = this.tag;
    }

    public bool IsInit()
    {
        if (CompareTag("data_int"))
        {
            return valueManager.intValues[dropdown.value - 1].isInit;
        }
        else if (CompareTag("data_bool"))
        {
            return valueManager.boolValues[dropdown.value - 1].isInit;
        }
        else if (CompareTag("data_string"))
        {
            return valueManager.stringValues[dropdown.value - 1].isInit;
        }
        return false;
    }
}
