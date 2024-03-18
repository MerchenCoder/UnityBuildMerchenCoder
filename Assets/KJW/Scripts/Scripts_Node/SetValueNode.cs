using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetValueNode : MonoBehaviour, INode, IFollowFlow
{
    public GameObject inPort;
    private ValueManager valueManager;
    private TMP_Dropdown dropdown;
    private DataInPort dataInPort;
    private NodeData nodeData;

    //node name
    private NodeNameManager nameManager;

    void Start()
    {
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "SetValueNode";
        nodeData = GetComponent<NodeData>();
        // valueManager = GameObject.Find("ValueManager").GetComponent<ValueManager>();
        valueManager = GetComponentInParent<Canvas>().GetComponentInChildren<ValueManager>();
        dropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
        dataInPort = inPort.GetComponent<DataInPort>();
    }

    IEnumerator INode.Execute()
    {
        if (!dataInPort.IsConnected)
        {
            Debug.Log("값 설정하기 노드 연결안됨");
            NodeManager.Instance.SetCompileError(true, "port");

            yield return null;
        }
        else
        {
            if (dropdown.value <= 1)
            {
                Debug.Log("값 설정하기 노드 변수 설정 안됨");
                NodeManager.Instance.SetCompileError(true, "value");
            }
            else
            {
                yield return dataInPort.connectedPort.SendData();
                if (CompareTag("data_int"))
                {
                    valueManager.intValues[dropdown.value - 2].isInit = true;
                    valueManager.intValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueInt;
                    nodeData.data_int = dataInPort.InputValueInt;
                }
                else if (CompareTag("data_bool"))
                {
                    valueManager.boolValues[dropdown.value - 2].isInit = true;
                    valueManager.boolValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueBool;
                    nodeData.data_bool = dataInPort.InputValueBool;
                }
                else if (CompareTag("data_string"))
                {
                    valueManager.stringValues[dropdown.value - 2].isInit = true;
                    valueManager.stringValues[dropdown.value - 2].valueOfValue = dataInPort.InputValueStr;
                    nodeData.data_string = dataInPort.InputValueStr;
                }
                else
                {
                    Debug.Log(gameObject.name + " 변수 태그 세팅 안됨");
                    NodeManager.Instance.SetCompileError(true, "개발 오류!!! 태그 설정 안됨");
                }
                GetComponent<NodeData>().ErrorFlag = false;
            }
        }
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }


    IEnumerator INode.ProcessData()
    {
        throw new System.NotImplementedException();
    }
}
