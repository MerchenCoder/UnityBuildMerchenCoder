using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePortsPosition : MonoBehaviour
{
    List<GameObject> nodes = new List<GameObject>();
    public void UpdatePortsPos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponentsInChildren<FlowoutPort>() != null)
            {
                FlowoutPort[] flowoutPorts = transform.GetChild(i).GetComponentsInChildren<FlowoutPort>();
                foreach (FlowoutPort flowoutPort in flowoutPorts)
                {
                    flowoutPort.UpdatePositionByScroll();
                }

            }
            if (transform.GetChild(i).GetComponentsInChildren<DataOutPort>() != null)
            {
                DataOutPort[] dataOutPorts = transform.GetChild(i).GetComponentsInChildren<DataOutPort>();
                foreach (DataOutPort dataOutPort in dataOutPorts)
                {
                    dataOutPort.UpdatePositionByScroll();
                }
            }
        }
    }
}
