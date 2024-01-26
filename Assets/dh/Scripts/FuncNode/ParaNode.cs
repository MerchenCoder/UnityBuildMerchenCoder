using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ParaNode : MonoBehaviour
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
    void SetParaNode(int type, string name) {
        //노드 nodeName 설정
        GetComponent<NodeNameManager>().NodeName = name;

        //ui 이름 반영
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;

        //타입 반영
        GameObject outPort = transform.GetChild(0).gameObject;
        switch(type){
            //0:int, 1:bool, 2=string
            case 0:
                //태그 설정
                outPort.tag = "data_int";
                //color 설정
                outPort.GetComponent<Image>().color = new Color(255,227,0);
            break;
            case 1:
            //태그 설정
                outPort.tag = "data_bool";
                //color 설정
                outPort.GetComponent<Image>().color = new Color(166,117,246);
            break;
            case 2:
            //태그 설정
                outPort.tag = "data_string";
                //color 설정
                outPort.GetComponent<Image>().color = new Color(242,158,73);
            break;
        }

    }
}
