using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReturnNode : MonoBehaviour, INode, IFollowFlow
{
    //node name
    private NodeNameManager nameManager;
    private NodeData nodeData;

    private DataInPort dataInPort;
    // Start is called before the first frame update
    void Start()
    {
        nameManager = GetComponent<NodeNameManager>();
        dataInPort = transform.GetChild(1).GetComponent<DataInPort>();

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

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();

    }

    public IEnumerator Execute()
    {
        //데이터 인포트 연결되어 있는지 확인
        //연결 안되어 있으면 바로 코루틴 중단해야함
        if (!dataInPort.IsConnected)
        {
            Debug.Log("반환노드에 반환할 데이터가 연결되어 있지 않음");
            NodeManager.Instance.SetCompileError(true);
            yield return null;
        }
        //데이터 인포트와 연결되어있는 포트로부터 값을 가져와야함(기다림)
        yield return dataInPort.connectedPort.GetComponent<DataOutPort>().SendData();


        //가져온 값을 반환처리 (해당 함수의 캔버스의 ForFunctionRunData 스크립트 변수에 저장해준다.)
        //혹시 모르니 해당 노드의 NodeData에도 저장을 해준다 (필요할까...?)
        if (dataInPort.tag == "data_int")
        {
            transform.parent.parent.GetComponent<ForFunctionRunData>().rt_int = dataInPort.InputValueInt;
            GetComponent<NodeData>().SetData_Int = dataInPort.InputValueInt;
            Debug.Log("반환 값은 : " + dataInPort.InputValueInt.ToString());

        }
        else if (dataInPort.tag == "data_bool")
        {
            transform.parent.parent.GetComponent<ForFunctionRunData>().rt_bool = dataInPort.InputValueBool;
            GetComponent<NodeData>().SetData_Bool = dataInPort.InputValueBool;
            Debug.Log("반환 값은 : " + dataInPort.InputValueBool.ToString());
        }

        else
        {
            transform.parent.parent.GetComponent<ForFunctionRunData>().rt_string = dataInPort.InputValueStr;
            GetComponent<NodeData>().SetData_string = dataInPort.InputValueStr;
            Debug.Log("반환 값은 : " + dataInPort.InputValueStr);
        }
        GetComponent<NodeData>().ErrorFlag = false;

        yield return null;

    }

    public IEnumerator ProcessData()
    {
        Debug.Log("Return Node calls ProcessData()");
        return null;
    }
}
