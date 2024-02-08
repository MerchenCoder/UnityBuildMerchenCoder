using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueNode : MonoBehaviour, INode
{
    private NodeNameManager nodeNameManager;
    [SerializeField] private DataOutPort outPort;

    public IEnumerator Execute()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        transform.GetChild(0).GetComponent<AddValueBtn>().OnChangeValue();
        GetComponent<NodeData>().ErrorFlag = false;
        yield return outPort.SendData();
    }

    void Start()
    {
        GetComponent<NodeData>().ErrorFlag = true;
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "GetValueNode";
    }
}