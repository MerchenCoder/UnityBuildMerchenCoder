using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlowinPort : MonoBehaviour
{
    private bool isConnected = false;
    [NonSerialized] public FlowoutPort connectedPort;

    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
        set
        {
            FlowConnectFlag(value);
            if (isConnected != value)
            {
                isConnected = value;

            }
        }
    }


    public void FlowConnectFlag(bool isConnected)
    {
        string nodeName;
        if (isConnected && FlagManager.instance != null)
        {
            nodeName = connectedPort.GetComponentInParent<NodeNameManager>().NodeName;
            if (GetComponentInParent<NodeNameManager>().NodeName == "PrintNode" && nodeName.Equals("SetValueNode") && FlagManager.instance.flagStr.Equals("ConnectFlow_SetValueNode"))
            {
                FlagManager.instance.OffFlag();
            }
        }

    }

}
