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
    private GameObject valueNameSettingUI_Parent;
    private TMPro.TMP_InputField inputField;
    // public GameObject valuePrefab;
    // private GameObject addedValue;

    private void Start()
    {
        //valueManager = GameObject.Find("ValueManager").GetComponent<ValueManager>();
        valueManager = GetComponentInParent<Canvas>().GetComponentInChildren<ValueManager>(true);
        if (GetComponentInParent<Canvas>(true).name == "MainCanvas")
        {
            valueNameSettingUI_Parent = GameObject.Find("Canvas_UI").transform.Find("ValueUI").gameObject;
        }
        else
        {
            valueNameSettingUI_Parent = GetComponentInParent<Canvas>(true).transform.Find("ValueUI").gameObject;
        }

        inputField = valueNameSettingUI_Parent.transform.GetChild(1).GetChild(1).GetComponent<TMP_InputField>();
        foreach (Transform child in valueNameSettingUI_Parent.transform)
        {
            child.gameObject.SetActive(false);
        }
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

            // 튜토리얼 플래그 추가 240522
            if (FlagManager.instance != null)
            {
                if (FlagManager.instance.flagStr == "AddValuable")
                {
                    FlagManager.instance.OffFlag();
                }
            }

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
        foreach (Transform child in valueNameSettingUI_Parent.transform)
        {
            child.gameObject.SetActive(true);
        }
        valueNameSettingUI_Parent.transform.GetChild(1).tag = this.tag;
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
