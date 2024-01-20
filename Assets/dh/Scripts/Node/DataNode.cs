using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNode : MonoBehaviour
{
    //node name 설정
    private NodeNameManager nameManager;

    private void Awake()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "DataNode";

    }
}
