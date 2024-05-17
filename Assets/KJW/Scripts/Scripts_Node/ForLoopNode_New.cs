using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForLoopNode_New : MonoBehaviour, INode, IFollowFlow
{
    private int loopIndex; //반복할 횟수(총 횟수)
    public int _index;//현재 반복 인덱스
    [SerializeField] private DataInPort dataInPort;
    [SerializeField] private DataOutPort dataOutPort;

    //변수 선언
    private GameObject loopCurrentNode;
    private FlowoutPort loopCurrentFlowoutPort;
    public bool isBreaking;//break 노드를 만났는지
    public bool isExcuting; //실행중일 때만 반복문 횟수를 data로 내보낼 수 있음
    public bool flowControlFlag;

    private void Start()
    {
        this.GetComponent<NodeNameManager>().NodeName = "ForLoopNode";
        _index = 0;
        isBreaking = false;
        flowControlFlag = false;
        isExcuting = false;
        GetComponent<NodeData>().ErrorFlag = true;

    }

    IEnumerator INode.Execute()
    {
        //1. isBreaking(break문 실행 여부), isExcuting 초기화
        isBreaking = false;
        isExcuting = true;
        flowControlFlag = false;

        //2. DataInPort 연결여부 확인
        if (!dataInPort.IsConnected)
        {
            //연결x
            Debug.Log("error1");
            Debug.Log("For 반복문 노드 반복횟수 연결 안됨");

            //오류시 항상 데이터 내보내지 못하게 처리
            isExcuting = false;
            GetComponent<NodeData>().ErrorFlag = true;

            NodeManager.Instance.SetCompileError(true, "port");
            yield break;
        }

        //3. 연결 o - DataInPort의 값을 가져와 loopIndex에 저장

        yield return dataInPort.connectedPort.SendData();
        loopIndex = dataInPort.InputValueInt;
        Debug.Log($"loopIndex : {loopIndex}");

        //4. 반복 조건 체크하기
        //현재 반복 횟수 < 총 반봇 횟수 && isBreaking이 false인 경우 반복한다.
        for (_index = 0; _index < loopIndex && !isBreaking; ++_index)
        {
            Debug.Log("For문 : " + (_index + 1) + "번째 실행");
            //현재 반복횟수 갱신
            GetComponent<NodeData>().data_int = _index + 1;

            //1. 반복문의 시작 노드를 찾기 위해 while 노드의 반복 flow outport를 찾는다.
            loopCurrentFlowoutPort = this.transform.Find("loopFlow").GetComponent<FlowoutPort>();
            //2. 반복문의 시작 노드 = flow outport에 연결된 노드
            loopCurrentNode = LoopNextNode(loopCurrentFlowoutPort);


            //loopCurrentNode가 없으면 오류 처리한다. 있으면 실행한다.
            while (loopCurrentNode != null)
            {
                //end 노드 만나면 break
                if (loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "EndNode")
                {
                    break;
                }
                //break 만나면 break;
                else if (loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "BreakNode")
                {
                    loopCurrentNode.GetComponent<BreakNode_New>().isForLoop = true;
                    loopCurrentNode.GetComponent<BreakNode_New>().loopStartNode = gameObject;
                    yield return loopCurrentNode.GetComponent<INode>().Execute();
                    break;
                }

                //if 노드 만나면 inner loop 처리를 위한 변수 설정
                else if (loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "IfNode")
                {
                    loopCurrentNode.GetComponent<IfNode_New>().isInnerLoop = true;
                    loopCurrentNode.GetComponent<IfNode_New>().isForLoop = true;
                    loopCurrentNode.GetComponent<IfNode_New>().loopStartNode = gameObject;
                }
                //현재 노드를 실행한다.
                yield return loopCurrentNode.GetComponent<INode>().Execute();

                //다음 port 찾기
                loopCurrentFlowoutPort = loopCurrentNode.GetComponent<IFollowFlow>().NextFlow();
                //loopCurrentNode 갱신하기
                loopCurrentNode = LoopNextNode(loopCurrentFlowoutPort);
                if (loopCurrentNode != null) Debug.Log(loopCurrentNode.name + " - " + (_index + 1).ToString());
            }

            //만약 flow outport에 연결된 노드가 없다면 && end 노드 연결이 없이 끝나면 오류 처리
            if (loopCurrentNode == null)
            {
                if (flowControlFlag)
                {
                    flowControlFlag = false;
                    break;
                }
                else
                {
                    Debug.Log("error2");
                    Debug.Log("Flow 연결에 문제가 있습니다.");
                    isExcuting = false;
                    NodeManager.Instance.SetCompileError(true, "flow");
                    yield break;
                }

            }

            yield return null;
        }
        isExcuting = false;
    }

    public GameObject LoopNextNode(FlowoutPort flowoutPort)
    {
        if (flowoutPort == null && loopCurrentNode.GetComponent<NodeNameManager>().NodeName == "IfNode")
        {
            IfNode_New ifNode = loopCurrentNode.GetComponent<IfNode_New>();
            if (ifNode.isBreaking)
            {
                flowControlFlag = true;
                return null;


            }
        }

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
        if (isExcuting)
        {
            GetComponent<NodeData>().ErrorFlag = false;
            yield return GetComponentInChildren<DataOutPort>().SendData();
            GetComponent<NodeData>().ErrorFlag = true;
        }
        else
        {
            Debug.Log("For문 외부에서 반복횟수 가져올 수 없음");
            NodeManager.Instance.SetCompileError(true, "반복문 외부에서 현재 반복 횟수를\n 사용할 수 없습니다.");
            yield return null;
        }
    }
}
