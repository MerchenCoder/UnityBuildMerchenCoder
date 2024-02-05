using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ParaNode : MonoBehaviour, INode
{
    //node name
    private NodeNameManager nameManager;
    private NodeData nodeData;
    public int paraNumber;

    // private FunctionManager functionManager;

    // Start is called before the first frame update
    void Start()
    {
        nameManager = GetComponent<NodeNameManager>();

    }


    //functionManager에서 호출해서 설정하도록 함.
    public void SetParaNode(int type, string name, int paraNum)
    {
        //노드 nodeName 설정
        GetComponent<NodeNameManager>().NodeName = "ParaNode";

        //ui 이름 반영
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;

        //타입 반영
        GameObject outPort = transform.GetChild(0).gameObject;
        switch (type)
        {
            //0:int, 1:bool, 2=string
            case 0:
                //태그 설정
                outPort.tag = "data_int";
                //color 설정
                outPort.GetComponent<Image>().color = new Color(0.949f, 0.835f, 0.290f);
                break;
            case 1:
                //태그 설정
                outPort.tag = "data_bool";
                //color 설정
                outPort.GetComponent<Image>().color = new Color(0.651f, 0.459f, 0.965f);
                break;
            case 2:
                //태그 설정
                outPort.tag = "data_string";
                //color 설정
                outPort.GetComponent<Image>().color = new Color(0.949f, 0.620f, 0.286f);
                break;
        }

        paraNumber = paraNum;

    }

    public IEnumerator Execute()
    {
        return null;
    }


    public IEnumerator ProcessData()
    {
        //데이터 받아오기
        GameObject outPort = transform.GetChild(0).gameObject;
        if (outPort.tag == "data_int")
        {
            if (paraNumber == 1)
                this.GetComponent<NodeData>().SetData_Int = transform.parent.parent.GetComponent<ForFunctionRunData>().p1_int;
            else
                GetComponent<NodeData>().SetData_Int = transform.parent.parent.GetComponent<ForFunctionRunData>().p2_int;
        }
        else if (outPort.tag == "data_bool")
        {
            if (paraNumber == 1)
                this.GetComponent<NodeData>().SetData_Bool = transform.parent.parent.GetComponent<ForFunctionRunData>().p1_bool;
            else
                GetComponent<NodeData>().SetData_Bool = transform.parent.parent.GetComponent<ForFunctionRunData>().p2_bool;
        }
        else
        {
            if (paraNumber == 1)
                this.GetComponent<NodeData>().SetData_string = transform.parent.parent.GetComponent<ForFunctionRunData>().p1_string;
            else
                GetComponent<NodeData>().SetData_string = transform.parent.parent.GetComponent<ForFunctionRunData>().p2_string;
        }
        GetComponent<NodeData>().ErrorFlag = false;

        yield return outPort.GetComponent<DataOutPort>().SendData();


    }
}
