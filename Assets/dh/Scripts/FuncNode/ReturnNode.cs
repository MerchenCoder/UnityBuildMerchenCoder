using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReturnNode : MonoBehaviour
{
    //node name
    private NodeNameManager nameManager;
    private NodeData nodeData;

    // private FunctionManager functionManager;

    // Start is called before the first frame update
    void Start()
    {
        // functionManager = GameObject.Find("FunctionManager").GetComponent<FunctionManager>();  
        nameManager = GetComponent<NodeNameManager>();
        // nameManager.NodeName = functionManager.FunName;

    }

    // Update is called once per frame
    void Update()
    {

    }


    //functionManager에서 호출해서 설정하도록 함.
    public void SetReturnNode(int type)
    {
        //노드 nodeName 설정
        GetComponent<NodeNameManager>().NodeName = "ReturnNode";

        //타입 반영
        GameObject inPort = transform.GetChild(1).gameObject;
        switch (type)
        {
            case 0:
                inPort.tag = "data_int";
                inPort.GetComponent<Image>().color = new Color(0.949f, 0.835f, 0.290f, 0.600f);

                break;
            case 1:
                //태그 설정
                inPort.tag = "data_bool";
                //color 설정
                inPort.GetComponent<Image>().color = new Color(0.651f, 0.459f, 0.965f, 0.600f);

                break;
            case 2:
                //태그 설정
                inPort.tag = "data_string";
                //color 설정
                inPort.GetComponent<Image>().color = new Color(0.949f, 0.620f, 0.286f, 0.600f);
                break;
        }

    }
}
