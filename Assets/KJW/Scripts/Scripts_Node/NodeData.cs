using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData : MonoBehaviour
{
    [NonSerialized] public int data_int;
    [NonSerialized] public string data_string;
    [NonSerialized] public bool data_bool;


    //임시로 node name 관련 코드 여기에 추가합니다.
    private NodeNameManager nameManager;

    private void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "DataNode";

    }



    //public void ReadNodeData()
    //{
    //    if (gameObject.CompareTag("data_int"))
    //    {

    //    }
    //    else if (gameObject.CompareTag("data_string"))
    //    {

    //    }
    //    else if (gameObject.CompareTag("data_bool"))
    //    {

    //    }
    //}
}
