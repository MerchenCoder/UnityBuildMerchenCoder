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
                if (outPort.isConnected)
                {
                    outPort.SendData();
                }
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

                if (outPort.isConnected)
                {
                    outPort.SendData();
                }
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
                if (outPort.isConnected)
                {
                    outPort.SendData();
                }
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
                // Debug.Log("errorFlag 변경");
                errorFlag = value;
                if (outPort.isConnected)
                {
                    outPort.SendData();
                    // Debug.Log("outPort가 inPort로 데이터 전달");
                }

            }
        }
    }

    private void Awake()
    {
        outPort = transform.Find("outPort").GetComponent<DataOutPort>();
        errorFlag = true;

        if (this.gameObject.CompareTag("Node_Int") || this.gameObject.CompareTag("Node_Bool") || this.gameObject.CompareTag("Node_String"))
        {
            errorFlag = false;
        }


    }
}
