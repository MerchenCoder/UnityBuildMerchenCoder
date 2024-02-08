using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileNode : MonoBehaviour, INode, IFollowFlow

{
    private bool loopCondition;
    int _index;
    [SerializeField] private DataInPort dataInPort;

    //변수 선언
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;
    [NonSerialized] public bool isBreaking;

    private void Start()
    {
        this.GetComponent<NodeNameManager>().NodeName = "ForLoopNode";
        isBreaking = false;
    }


    //다음 노드 반환하는 메소드
    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        return flowoutPort.ConnectedPort.transform.parent.gameObject;
    }

    // 실행함수 완전히 종료 후 종료플로우
    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    IEnumerator INode.Execute()
    {
        if (!dataInPort.IsConnected)
        {
            Debug.Log("While 반복문 노드 연결 안됨");
            NodeManager.Instance.SetCompileError(true);

            yield return null;
        }
        else
        {
            yield return dataInPort.connectedPort.SendData();
            loopCondition = dataInPort.InputValueBool;
        }
        //if (dataInPort.connectedPort.transform.parent.GetComponent<GetValueNode>() != null)
        //{
        //    dataInPort.connectedPort.transform.parent.GetComponent<GetValueNode>().BringValueData();
        //}
        //loopCondition = dataInPort.InputValueBool;

        for (_index = 0; loopCondition && !isBreaking; ++_index)
        {

            Debug.Log(_index + 1 + "번째 실행");
            //반복 시작 노드의 Flow outPort 찾기
            currentFlowoutPort = this.transform.Find("loopFlow").GetComponent<FlowoutPort>();
            //Flow loopPort로 반복내용 node 찾아서 currentNode 업데이트
            currentNode = NextNode(currentFlowoutPort);

            // 실행
            Debug.Log(currentNode);
            yield return currentNode.GetComponent<INode>().Execute();

            // 조건 bool값 다시 확인
            yield return dataInPort.connectedPort.SendData();
            loopCondition = dataInPort.InputValueBool;

            //다음 노드 없으면 처음부터 실행
            while (currentNode.transform.Find("outFlow").GetComponent<FlowoutPort>().IsConnected)
            {
                //현재 노드의 Flow outPort 찾기
                currentFlowoutPort = currentNode.GetComponent<IFollowFlow>().NextFlow();
                //Flow outPort로 다음 node 찾아서 currentNode 업데이트
                currentNode = NextNode(currentFlowoutPort);
                if (currentNode.CompareTag("Node_Break"))
                {
                    currentNode.GetComponent<BreakNode>().isWhileLoop = true;
                    currentNode.GetComponent<BreakNode>().loopStartNode = this.gameObject;
                    isBreaking = true;
                    break;
                }
                else yield return currentNode.GetComponent<INode>().Execute();
            }
            yield return null;
        }

    }

    public IEnumerator ProcessData()
    {
        throw new NotImplementedException();
    }
}

