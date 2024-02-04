using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData : MonoBehaviour
{
    [NonSerialized] public int data_int;
    [NonSerialized] public string data_string;
    [NonSerialized] public bool data_bool;


    public int SetData_Int
    {
        set
        {
            if (data_int != value)
            {
                data_int = value;
            }
        }
    }
    public bool SetData_Bool
    {
        set
        {
            if (data_bool != value)
            {
                data_bool = value;
            }
        }
    }
    public string SetData_string
    {
        set
        {
            if (data_string != value)
            {
                data_string = value;
            }
        }
    }

    private DataOutPort outPort;
    private bool errorFlag;

    public bool ErrorFlag
    {
        get
        {
            return errorFlag;
        }
        set
        {
            if (errorFlag != value)
            {
                errorFlag = value;
            }
        }
    }

    private void Start()
    {
        outPort = transform.Find("outPort").GetComponent<DataOutPort>();
        errorFlag = true;

        if (GetComponent<NodeNameManager>().NodeName == "DataNode")
        {
            errorFlag = false;
        }


    }
}
