using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class IfNode_New : MonoBehaviour, INode, IFollowFlow
{
    // node name
    private NodeNameManager nameManager;

    // nodeData
    private NodeData nodeData;

    public bool condition; //조건

    [SerializeField] FlowoutPort trueFlowOutPort; //참 flow out port
    [SerializeField] FlowoutPort falseFlowOutPort; //거짓 flow out port
    [SerializeField] FlowoutPort nextFlowOutPort; //next flow out port
    private bool trueConntected;
    private bool falseConnected;
    private bool nextConnected;

    public bool isBreaking;
    public bool flowControlFlag;
    // DataInputPort 클래스 참조

    [SerializeField] private GameObject ifCurrentNode;
    [SerializeField] private FlowoutPort ifCurrentFlowoutPort;

    [SerializeField] private DataInPort dataInPort;


    [Header("Inner Loop Contorl")]
    public bool isInnerLoop = false;
    public bool isWhileLoop = false;
    public bool isForLoop = false;
    public GameObject loopStartNode = null;

    void Start()
    {
        // node name
        nameManager = this.GetComponent<NodeNameManager>();
        nameManager.NodeName = "IfNode";

        // node data
        nodeData = GetComponent<NodeData>();

        //inner loop 변수 초기화
        ResetInnerLoopVariables();

        isBreaking = false;
        flowControlFlag = false;

    }

    IEnumerator INode.Execute()
    {
        //1. break 실행 여부 초기화
        isBreaking = false;
        flowControlFlag = false;
        //2. DataInPort 연결 여부 확인
        if (!dataInPort.IsConnected)
        {
            //연결 x
            Debug.Log("If 노드 조건 연결 안됨");
            ResetInnerLoopVariables();
            NodeManager.Instance.SetCompileError(true, "port");
            yield break;
        }
        //3. 연결 o - DataInPort의 값을 가져와 조건 변수에 저장
        yield return dataInPort.connectedPort.GetComponent<DataOutPort>().SendData();
        condition = dataInPort.InputValueBool;


        //4.플로우 아웃 포트 연결 여부 확인하기
        //4-1. 플로우 아웃 포트 연결 여부 저장
        trueConntected = trueFlowOutPort.IsConnected;
        falseConnected = falseFlowOutPort.IsConnected;
        nextConnected = nextFlowOutPort.IsConnected;

        //4-2. 연결 안된 경우 오류 처리
        if (!nextConnected)
        {
            Debug.Log("초록색 OutFlow는 연결되어야 합니다!");
            ResetInnerLoopVariables();
            NodeManager.Instance.SetCompileError(true, "flow");
            yield break;
        }
        else if (!trueConntected && !falseConnected)
        {
            Debug.Log("Outport 중 하나는 연결되어야 합니다!");
            ResetInnerLoopVariables();
            NodeManager.Instance.SetCompileError(true, "flow");
            yield break;
        }

        //5. 조건 판별 후 ifCurrentFlowoutPort, ifCurrentNode 설정하기
        ifCurrentFlowoutPort = condition ? trueFlowOutPort : falseFlowOutPort;
        ifCurrentNode = IfNextNode(ifCurrentFlowoutPort);

        //6. if node는 참/거짓 플로우가 endnode를 만나거나 break문을 만날때까지 플로우를 따라간다.
        //플로우가 end, break 노드 없이 끝나는 경우 오류처리
        while (ifCurrentNode != null)
        {
            Debug.Log("aaaaasdfja;lflasf");
            //end node인 경우
            if (ifCurrentNode.GetComponent<NodeNameManager>().NodeName == "EndNode")
            {
                break;
            }
            //break node인 경우
            if (ifCurrentNode.GetComponent<NodeNameManager>().NodeName == "BreakNode")
            {
                if (isInnerLoop && loopStartNode != null)
                {
                    ifCurrentNode.GetComponent<BreakNode_New>().loopStartNode = loopStartNode;
                    if (isForLoop)
                    {
                        ifCurrentNode.GetComponent<BreakNode_New>().isForLoop = true;

                    }
                    else if (isWhileLoop)
                    {
                        ifCurrentNode.GetComponent<BreakNode_New>().isWhileLoop = true;
                    }
                    yield return ifCurrentNode.GetComponent<INode>().Execute();
                    isBreaking = true;
                    ResetInnerLoopVariables();
                    yield break;
                }
            }
            //중첩 조건문인 경우
            else if (ifCurrentNode.GetComponent<NodeNameManager>().NodeName == "IfNode")
            {
                Debug.Log("dafdasfasdfdasfasfasf");
                IfNode_New innerIfNode = ifCurrentNode.GetComponent<IfNode_New>();
                innerIfNode.isInnerLoop = isInnerLoop;
                innerIfNode.isWhileLoop = isWhileLoop;
                innerIfNode.isForLoop = isForLoop;
                innerIfNode.loopStartNode = loopStartNode;
            }
            //현재 노드를 실행한다.
            yield return ifCurrentNode.GetComponent<INode>().Execute();

            //다음 포트 찾기
            ifCurrentFlowoutPort = ifCurrentNode.GetComponent<IFollowFlow>().NextFlow();

            ifCurrentNode = IfNextNode(ifCurrentFlowoutPort);
            if (ifCurrentNode != null)
            {
                Debug.Log($"ifNode 조건에 따른 플로우 실행중. 다음 실행할 노드 :{ifCurrentNode.name} ");
            }
        }

        //오류 처리
        if (ifCurrentNode == null)
        {

            if (isBreaking)
            {
                yield break;
            }
            Debug.Log("Flow 연결에 문제가 있습니다.");
            NodeManager.Instance.SetCompileError(true, "flow");
            ResetInnerLoopVariables();
            yield break;
        }

        //7. if문 실행이 끝나면 innerloop 관련 변수 설정을 초기화한다.
        //반복문에서 if문을 만나면 innerloop 변수를 설정해주고 -> if문이 실행된다. 
        //따라서 실행이 끝나고 reset해주어야 한다.
        ResetInnerLoopVariables();
        yield return null;
    }

    public GameObject IfNextNode(FlowoutPort flowoutPort)
    {
        if (flowoutPort == null)
        {

            if (ifCurrentNode.GetComponent<NodeNameManager>().NodeName == "IfNode")
            {
                if (ifCurrentNode.GetComponent<IfNode_New>().isBreaking)
                {

                    isBreaking = true;
                    StopAllCoroutines();
                }
                return null;
            }
        }

        if (flowoutPort.ConnectedPort != null)
            return flowoutPort.ConnectedPort.transform.parent.gameObject;
        else
        {
            return null;
        }
    }

    // 실행함수 완전히 종료 후 종료플로우 반환
    public FlowoutPort NextFlow()
    {
        if (isBreaking)
        {

            return null;
        }

        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    public IEnumerator ProcessData()
    {
        throw new NotImplementedException();
    }

    private void ResetInnerLoopVariables()
    {
        isInnerLoop = false;
        isWhileLoop = false;
        isForLoop = false;
        loopStartNode = null;

    }
}