using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


//CalcNode.cs 변형한 스크립트. 자세한 설명은 CalcNode.cs의 주석 참고
public class CompNode : MonoBehaviour, INode
{
    public int method;
    //0:같다 1:같지 않다 
    //2:크다 3:크거나 같다
    //4:작다 5:작거나 같다

    //DataInputPort Class
    private DataInPort dataInPort1;
    private DataInPort dataInPort2;

    //node name
    private NodeNameManager nameManager;

    //nodeData
    private NodeData nodeData;
    private NodeManager nodeManager;


    private int n1;
    private int n2;


    private bool b1;
    private bool b2;

    private string s1;
    private string s2;



    void Start()
    {
        //inPort
        dataInPort1 = transform.GetChild(1).GetComponent<DataInPort>();
        dataInPort2 = transform.GetChild(2).GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        // dataInPort1.StateChanged += HandleStateChanged;
        // dataInPort2.StateChanged += HandleStateChanged;


        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "CompNode";
        nodeManager = GameObject.FindFirstObjectByType<NodeManager>();


        //node data
        nodeData = GetComponent<NodeData>();
    }

    bool CompData(int method, int input1, int input2)
    {
        bool result = false;
        switch (method)
        {
            case 2:
                result = input1 > input2;
                break;
            case 3:
                result = input1 >= input2;
                break;
            case 4:
                result = input1 < input2;
                break;
            case 5:
                result = input1 <= input2;
                break;
        }
        // When the operation completes successfully, it is necessary to update the errorFlag of the current node itself to false
        nodeData.ErrorFlag = false;
        return result;
    }

    bool CompEqual(int method, int type)
    {
        bool result = false;
        switch (method)
        {
            case 0:
                if (type == 0)
                {
                    result = n1 == n2;
                }
                else if (type == 1)
                {
                    result = b1 == b2;
                }
                else
                {
                    result = s1.Equals(s2);
                }

                break;
            case 1:
                if (type == 0)
                {
                    result = n1 != n2;
                }
                else if (type == 1)
                {
                    result = b1 != b2;
                }
                else
                {
                    result = !s1.Equals(s2);
                }
                break;
        }
        nodeData.ErrorFlag = false;
        return result;

    }

    public IEnumerator Execute()
    {
        throw new NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
        {
            Debug.Log("비교연산노드 연결 모두 안됨");
            NodeManager.Instance.SetCompileError(true, "port");
            yield break;

        }
        else if (dataInPort1.connectedPort.tag != dataInPort2.connectedPort.tag)
        {
            Debug.Log("포트 간 자료형 다름");
            NodeManager.Instance.SetCompileError(true, "data type");
            yield break;
        }
        else
        {

            yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
            yield return dataInPort2.connectedPort.GetComponent<DataOutPort>().SendData();
            if (method <= 1)
            {
                //같다, 같지 않다는 모든 자료형이 가능함
                if (dataInPort1.connectedPort.CompareTag("data_int"))
                {
                    n1 = dataInPort1.InputValueInt;
                    n2 = dataInPort2.InputValueInt;
                    nodeData.SetData_Bool = CompEqual(method, 0);

                }
                else if (dataInPort1.connectedPort.CompareTag("data_bool"))
                {
                    b1 = dataInPort1.InputValueBool;
                    b2 = dataInPort2.InputValueBool;
                    nodeData.SetData_Bool = CompEqual(method, 1);


                }
                else if (dataInPort1.connectedPort.CompareTag("data_string"))
                {
                    s1 = dataInPort1.InputValueStr;
                    s2 = dataInPort2.InputValueStr;
                    nodeData.SetData_Bool = CompEqual(method, 2);

                }
                else
                {
                    Debug.Log("dataInPort tag 오류");
                    NodeManager.Instance.SetCompileError(true, "포트 태그 오류!! 개발 오류!!");
                }

            }
            else
            {
                n1 = dataInPort1.InputValueInt;
                n2 = dataInPort2.InputValueInt;
                nodeData.SetData_Bool = CompData(method, n1, n2);

            }

            yield return GetComponentInChildren<DataOutPort>().SendData();
            nodeData.ErrorFlag = true;
        }
    }

}
