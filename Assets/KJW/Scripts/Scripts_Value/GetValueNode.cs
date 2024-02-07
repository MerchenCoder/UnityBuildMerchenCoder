using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueNode : MonoBehaviour
{
    private AddValueBtn addValueAllowBtn;
    private NodeNameManager nodeNameManager;
    [SerializeField] private DataOutPort outPort;

    void Start()
    {
        addValueAllowBtn = transform.GetChild(0).GetComponent<AddValueBtn>();
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "DataNode";
    }

    public void BringValueData()
    {
        addValueAllowBtn.OnChangeValue();
        outPort.SendData();
    }

}