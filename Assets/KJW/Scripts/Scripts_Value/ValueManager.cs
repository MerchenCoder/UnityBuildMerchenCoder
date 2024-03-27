using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.UI;

[System.Serializable]
public class IntValue
{
    public string valueName;
    public int valueOfValue;
    public bool isInit;
}

[System.Serializable]
public class BoolValue
{
    public string valueName;
    public bool valueOfValue;
    public bool isInit;
}

[System.Serializable]
public class StrValue
{
    public string valueName;
    public string valueOfValue;
    public bool isInit;
}


public class ValueManager : MonoBehaviour
{
    public List<IntValue> intValues = new List<IntValue>();
    public List<BoolValue> boolValues = new List<BoolValue>();
    public List<StrValue> stringValues = new List<StrValue>();

    public event EventHandler OnUpdateValues;

    public int lastAddedType;

    private void Start()
    {

    }
    public void AddValue(int type, string name)
    {
        lastAddedType = type;
        /* 리스트에 추가 */
        // type 0:int 1:bool 2:string // 추후 enum으로 변경
        if (type == 0)
        {
            IntValue intValue = new IntValue();
            intValue.valueName = name;
            intValue.valueOfValue = 0;
            intValue.isInit = false;
            intValues.Add(intValue);
        }
        if (type == 1)
        {
            BoolValue boolValue = new BoolValue();
            boolValue.valueName = name;
            boolValue.valueOfValue = true;
            boolValue.isInit = false;
            boolValues.Add(boolValue);
        }
        if (type == 2)
        {
            StrValue strValue = new StrValue();
            strValue.valueName = name;
            strValue.valueOfValue = null;
            strValue.isInit = false;
            stringValues.Add(strValue);
        }
        OnUpdateValues?.Invoke(this, EventArgs.Empty);
    }

    /* 필요 함수
    // 이름으로 변수의 값 가져오기
    */

    public bool isExistValue(string name)
    {
        for (int i = 0; i < intValues.Count; i++)
        {
            if (intValues[i].valueName == name)
            {
                return true;
            }
        }
        for (int i = 0; i < boolValues.Count; i++)
        {
            if (boolValues[i].valueName == name)
            {
                return true;
            }
        }
        for (int i = 0; i < stringValues.Count; i++)
        {
            if (stringValues[i].valueName == name)
            {
                return true;
            }
        }
        return false;
    }


    public void ChangeValueOfIntValue(string name, int value)
    {
        for (int i = 0; i < intValues.Count; i++)
        {
            if (intValues[i].valueName == name)
            {
                intValues[i].valueOfValue = value;
                break;
            }
        }
    }

    public void ChangeValueOfBoolValue(string name, bool value)
    {
        for (int i = 0; i < boolValues.Count; i++)
        {
            if (boolValues[i].valueName == name)
            {
                boolValues[i].valueOfValue = value;
                break;
            }
        }
    }

    public void ChangeValueOfStrValue(string name, string value)
    {
        for (int i = 0; i < stringValues.Count; i++)
        {
            if (stringValues[i].valueName == name)
            {
                stringValues[i].valueOfValue = value;
                break;
            }
        }
    }
}
