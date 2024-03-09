using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InputNode : MonoBehaviour, INode
{
    private NodeNameManager nameManager;
    private NodeData nodeData;
    public string inputNodeName;
    public int inputIndex;
    string dataTypeTag;




    // Start is called before the first frame update
    private void Awake()
    {
        nameManager = GetComponent<NodeNameManager>();
        nameManager.NodeName = "InputNode";

        nodeData = GetComponent<NodeData>();
    }
    void Start()
    {
        dataTypeTag = GetComponentInChildren<DataOutPort>().tag;

    }

    // Update is called once per frame
    void Update()
    {


    }

    public IEnumerator Execute()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        string data = TestManager.Instance.currentInput[inputIndex];
        data = data.Trim(); //혹시 모르니 공백 제거
        //testManager로부터 현재 케이스로 설정된 입력데이터 값을 가져와야 함.
        if (dataTypeTag == "data_bool")
        {
            nodeData.SetData_Bool = bool.Parse(data);
            nodeData.ErrorFlag = false;
        }
        else if (dataTypeTag == "data_int")
        {
            nodeData.SetData_Int = int.Parse(data);
            nodeData.ErrorFlag = false;

        }
        else if (dataTypeTag == "data_string")
        {
            nodeData.SetData_string = data;
            nodeData.ErrorFlag = false;

        }
        else
        {
            Debug.Log("ProcessData() 실행 중 실패");
            yield break;
        }


        yield return GetComponentInChildren<DataOutPort>().SendData();
        nodeData.ErrorFlag = true;
    }

}
