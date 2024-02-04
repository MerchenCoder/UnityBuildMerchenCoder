using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class IfNode : MonoBehaviour, IFollowFlow, INode
{
    private NodeNameManager nameManager;

    //inPort
    private GameObject inPort1;
    private GameObject outputData;

    //operand
    private bool input1;

    //operand gameobject
    private GameObject input1Val;

    //DataInputPort 클래스 참조
    private DataInPort dataInPort1;


    public FlowoutPort NextFlow()
    {
        if (input1 == true)
        {
            //Debug.Log("conclusion: input1 is true");
            return transform.Find("outputTrueFlow").GetComponent<FlowoutPort>();
        }
        else
        {
            //Debug.Log("conclusion: input1 is false");
            return transform.Find("outputFalseFlow").GetComponent<FlowoutPort>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inPort1 = transform.GetChild(4).gameObject;
        //Debug.Log("inport1 is " + inPort1);

        dataInPort1 = inPort1.GetComponent<DataInPort>();

        //DataInputPort 클래스의 StateChanged 이벤트에 이벤트 핸들러 메서드 등록;
        dataInPort1.StateChanged += HandleStateChanged;
        //node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "IfNode";
    }
    void HandleStateChanged(object sender, InputPortStateChangedEventArgs e)
    {
        //inputPort에 연결된 StateChanged 이벤트에서 isConnected가 바뀌면 이벤트가 발생
        //두 포트의 isConnected가 true로 바뀔때만 계산함.
        //가져온 값을 각각 input1로 할당하고 호출 예정
        if (e.IsConnected)
        {
            Debug.Log("inport1 is connected");
            if (inPort1.GetComponent<DataInPort>().IsConnected)
            {
                // inputPort1과 inputPort2가 연결되어 있을 때만 계산 수행
                input1 = inPort1.GetComponent<DataInPort>().InputValueBool;
                //Debug.Log("connected inport1 is " + input1);
            }
            NextFlow();
        }
        else
        {
            //Debug.Log("inport1 is disconnected");
        }

    }

    IEnumerator INode.Execute()
    {
        throw new NotImplementedException();
    }


    IEnumerator INode.ProcessData()
    {
        throw new NotImplementedException();
    }
}
