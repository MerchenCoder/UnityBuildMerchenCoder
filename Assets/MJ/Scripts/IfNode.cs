using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class IfNode : MonoBehaviour, INode
{
    // node name
    private NodeNameManager nameManager;

    // nodeData
    private NodeData nodeData;
    private bool n1;

    // DataInputPort 클래스 참조
    private DataInPort dataInPort1;

    void Start()
    {
        // inPort
        dataInPort1 = transform.GetChild(4).GetComponent<DataInPort>(); //data 받아옴

        // node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "IfNode";

        // node data
        nodeData = GetComponent<NodeData>();
    }

    //public bool NextFlow()
    //{
    //    if (input1 == true)
    //    {
    //        Debug.Log("conclusion: input1 is true");
    //        nodeData.ErrorFlag = false;
    //        flowout = transform.Find("outputTrueFlow").GetComponent<FlowoutPort>();
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("conclusion: input1 is false");
    //        nodeData.ErrorFlag = false;
    //        flowout = transform.Find("outputFalseFlow").GetComponent<FlowoutPort>();
    //        return false;
    //    }
    //}

    public IEnumerator Execute()
    {
        throw new NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected)
        {
            Debug.Log("Data 노드 연결 안됨");
            NodeManager.Instance.SetCompileError(true);
            yield return null;
        }
        else
        {
            Debug.Log("Data 노드 연결 됨");
            yield return dataInPort1.connectedPort.GetComponent<DataOutPort>().SendData();
            n1 = dataInPort1.InputValueBool;
            //yield return GetComponentInChildren<DataOutPort>().SendData();
            if (n1 == true)
            {
                Debug.Log("conclusion: n1 is true");
                nodeData.ErrorFlag = false;
                yield return transform.Find("outputTrueFlow").GetComponentInChildren<DataOutPort>().SendData();
            }
            else
            {
                Debug.Log("conclusion: n1 is false");
                nodeData.ErrorFlag = false;
                yield return transform.Find("outputFalseFlow").GetComponentInChildren<DataOutPort>().SendData();
            }
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using TMPro;


//public class IfNode : MonoBehaviour, IFollowFlow, INode
//{
//    private NodeNameManager nameManager;

//    //inPort
//    private GameObject inPort1;
//    private GameObject outputData;

//    //operand
//    private bool input1;

//    //operand gameobject
//    private GameObject input1Val;

//    //DataInputPort 클래스 참조
//    private DataInPort dataInPort1;


//    public FlowoutPort NextFlow()
//    {
//        if (input1 == true)
//        {
//            Debug.Log("conclusion: input1 is true");
//            return transform.Find("outputTrueFlow").GetComponent<FlowoutPort>();
//        }
//        else
//        {
//            Debug.Log("conclusion: input1 is false");
//            return transform.Find("outputFalseFlow").GetComponent<FlowoutPort>();
//        }
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        inPort1 = transform.GetChild(4).gameObject;
//        Debug.Log("inport1 is " + inPort1);

//        dataInPort1 = inPort1.GetComponent<DataInPort>();

//        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
//        dataInPort1.StateChanged += HandleStateChanged;
//        //node name
//        nameManager = this.GetComponent<NodeNameManager>();
//        nameManager.NodeName = "IfNode";
//    }
//    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
//    {
//        //inputPort에 연결된 StateChanged 이벤트에서 isConnected가 바뀌면 이벤트가 발생
//        //두 포트의 isConnected가 true로 바뀔때만 계산함.
//        //가져온 값을 각각 input1로 할당하고 호출 예정
//        if (e.IsConnected)
//        {
//            Debug.Log("inport1 is connected");
//            if (inPort1.GetComponent<DataInPort>().IsConnected)
//            {
//                // inputPort1이 연결되어 있을 때만 계산 수행
//                input1 = inPort1.GetComponent<DataInPort>().InputValueBool;
//                Debug.Log("connected inport1 is " + input1);
//            }
//            NextFlow();
//        }
//        else
//        {
//            Debug.Log("inport1 is disconnected");
//        }

//    }

//    IEnumerator INode.Execute()
//    {
//        throw new NotImplementedException();
//    }


//    IEnumerator INode.ProcessData()
//    {
//        throw new NotImplementedException();
//    }
//}