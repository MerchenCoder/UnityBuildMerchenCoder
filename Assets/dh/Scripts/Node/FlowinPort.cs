using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlowinPort : MonoBehaviour
{
    private bool isConnected = false;
    [NonSerialized] public outFlow connectedPort;

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
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
