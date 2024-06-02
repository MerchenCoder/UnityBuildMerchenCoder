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
            if (isConnected != value)
            {
                isConnected = value;
                FlowConnectFlag();
            }
        }
    }


    public void FlowConnectFlag()
    {
        // string 
        // if(isConnected && FlagManager.instance.flagStr)
    }

}
