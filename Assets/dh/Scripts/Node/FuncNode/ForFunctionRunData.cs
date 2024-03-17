using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForFunctionRunData : MonoBehaviour
{
    public int p1_int;
    public int p2_int;
    public bool p1_bool;
    public bool p2_bool;
    public string p1_string;
    public string p2_string;

    public int rt_int;
    public bool rt_bool;
    public string rt_string;

    public void SetParaValue(DataInPort dataInPort, int num)
    {
        if (dataInPort.CompareTag("data_int"))
        {
            if (num == 1)
            {
                p1_int = dataInPort.InputValueInt;
            }
            else
            {
                p2_int = dataInPort.InputValueInt;
            }
        }
        else if (dataInPort.CompareTag("data_bool"))
        {
            if (num == 1)
            {
                p1_bool = dataInPort.InputValueBool;
            }
            else
            {
                p2_bool = dataInPort.InputValueBool;
            }
        }
        else
        {
            if (num == 1)
            {
                p1_string = dataInPort.InputValueStr;
            }
            else
            {
                p2_string = dataInPort.InputValueStr;
            }

        }
    }
}
