using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileNode_New : MonoBehaviour, INode, IFollowFlow
{
    private bool loopCondition; //조건을 만족하는지 체크
    private int _index; //반복 횟수
    [SerializeField] private DataInPort dataInPort; //조건 데이터


    //내부 반복 흐름 처리
    private GameObject loopCurrentNode;
    private FlowoutPort loopCurrentFlowoutPort;
    //[NonSerialized] 
    public bool isBreaking; //break 노드를 만났는지

    private void Start()
    {
        this.GetComponent<NodeNameManager>().NodeName = "WhileLoopNode";
        isBreaking = false;
        _index = 0;
        isBreaking = false;
    }

    IEnumerator INode.Execute()
    {
        //1. isBreaking(break문 실행 여부) 초기화
        isBreaking = false;

        //2. DataInPort 연결여부 확인
        if (!dataInPort.IsConnected)
        {   //연결 x
            Debug.Log("error1");
            Debug.Log("While 반복문 노드 반복횟수 연결 안됨");
            NodeManager.Instance.SetCompileError(true, "port");
            yield break;
        }

        //3. 연결 o - DataInPort의 값을 가져와 조건 변수에 저장
        else
        {
            yield return dataInPort.connectedPort.SendData();
            loopCondition = dataInPort.InputValueBool;
        }

        //4. 반복 조건 체크하기 (매 반복 마지막에 조건 갱신하기)
        //-> 조건이 참이고 isBreaking이 false인 경우 반복한다.
        for (_index = 0; loopCondition && !isBreaking; ++_index)
        {
            Debug.Log("While문 : " + (_index + 1) + "번째 실행");

            /*  반복 플로우를 따라 실행한다.
                end 노드를 만날 때까지 플로우를 따라간다. 
                end 노드가 없이 플로우가 끝나는 경우 플로우 오류 처리한다.
            */

            //1. 반복문의 시작 노드를 찾기 위해 while 노드의 반복 flow outport를 찾는다.
            //2. 반복문의 시작 노드 = flow outport에 연결된 노드
            //만약 flow outport에 연결된 노드가 없다면 오류 처리한다.
            loopCurrentFlowoutPort = transform.Find("loopFlow").GetComponent<FlowoutPort>();
            loopCurrentNode = LoopNextNode(loopCurrentFlowoutPort);

            //loopCurrentNode가 없으면 오류 처리한다. 있으면 실행한다.
            while (loopCurrentNode != null)
            {
                //end노드 만나면 break;
                if (loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "EndNode")
                {
                    break;
                }
                //break 만나면 break;(break 노드가 실행될 때 isBreaking 처리됨)
                else if (loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "BreakNode")
                {
                    loopCurrentNode.GetComponent<BreakNode_New>().isWhileLoop = true;
                    loopCurrentNode.GetComponent<BreakNode_New>().loopStartNode = gameObject;
                    yield return loopCurrentNode.GetComponent<INode>().Execute();
                    break;
                }
                //if노드 만나면 이중반복문 변수 설정
                else if (loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "IfNode")
                {
                    loopCurrentNode.GetComponent<IfNode>().isInnerLoop = true;
                    loopCurrentNode.GetComponent<IfNode>().isWhileLoop = true;
                    loopCurrentNode.GetComponent<IfNode>().loopStartNode = gameObject;
                }
                //현재 노드를 실행한다.
                yield return loopCurrentNode.GetComponent<INode>().Execute();

                //다음 port 찾기
                loopCurrentFlowoutPort = loopCurrentNode.GetComponent<IFollowFlow>().NextFlow();
                //loopCurrentNode 갱신하기
                loopCurrentNode = LoopNextNode(loopCurrentFlowoutPort);
                if (loopCurrentNode != null) Debug.Log(loopCurrentNode.name + " - " + _index.ToString());

            }
            //end 노드 연결이 없이 끝나면 오류 처리
            if (loopCurrentNode == null)
            {
                Debug.Log("error2");
                Debug.Log("Flow 연결에 문제가 있습니다.");
                NodeManager.Instance.SetCompileError(true, "flow");
                yield break;
            }
            //반복 1회 종료 후 loopCondition 갱신
            yield return dataInPort.connectedPort.SendData();
            loopCondition = dataInPort.InputValueBool;
            yield return null;
        }
    }


    //반복문 내에세 다음으로 실행할 노드 반환하는 메소드
    //break 만나면 -> break 실행 후
    //end 만나면 -> 반복문 
    public GameObject LoopNextNode(FlowoutPort flowoutPort)
    {
        if (flowoutPort.ConnectedPort != null)
            return flowoutPort.ConnectedPort.transform.parent.gameObject;
        else return null;
    }

    // 실행함수 완전히 종료 후 종료플로우 반환
    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    public IEnumerator ProcessData()
    {
        throw new NotImplementedException();
    }
}

