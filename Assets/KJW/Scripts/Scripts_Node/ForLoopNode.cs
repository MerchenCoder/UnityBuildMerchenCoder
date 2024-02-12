using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForLoopNode : MonoBehaviour, INode, IFollowFlow
{
    private int loopIndex;
    public int _index;
    [SerializeField] private DataInPort dataInPort;
    [SerializeField] private DataOutPort dataOutPort;

    //변수 선언
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;
    [NonSerialized] public bool isBreaking;

    private void Start()
    {
        this.GetComponent<NodeNameManager>().NodeName = "ForLoopNode";
    }

    //다음 노드 반환하는 메소드
    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        if(flowoutPort.ConnectedPort != null)
            return flowoutPort.ConnectedPort.transform.parent.gameObject;
        else return null;
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
            Debug.Log("For 반복문 노드 반복횟수 연결 안됨");
            NodeManager.Instance.SetCompileError(true);

            yield return null;
        }
        else
        {
            yield return dataInPort.connectedPort.SendData();
            // dataInPort에서 inputValue 가져오기
            loopIndex = dataInPort.InputValueInt;
        }
        _index = 0;
        GetComponent<NodeData>().ErrorFlag = false;
        for (_index = 0; _index < loopIndex && !isBreaking; ++_index)
        {
            Debug.Log(_index + 1 + "번째 실행");
            GetComponent<NodeData>().data_int = _index + 1;
            //반복 시작 노드의 Flow outPort 찾기
            currentFlowoutPort = this.transform.Find("loopFlow").GetComponent<FlowoutPort>();
            //Flow loopPort로 반복내용 node 찾아서 currentNode 업데이트
            currentNode = NextNode(currentFlowoutPort);
            if (dataOutPort.isConnected) dataOutPort.SendData();

            // 실행
            if(currentNode != null)
                yield return currentNode.GetComponent<INode>().Execute();
            else
            {
                Debug.Log("For 반복문 노드 반복내용 연결 안됨");
                NodeManager.Instance.SetCompileError(true);

                yield return null;
            }
            if (currentNode.CompareTag("endNode"))
            {
                Debug.Log("For 반복문 노드 끝 노드에 연결됨");
                NodeManager.Instance.SetCompileError(true);
                isBreaking = true;
                break;
            }
            if (currentNode.CompareTag("Node_Break"))
            {
                currentNode.GetComponent<BreakNode>().isForLoop = true;
                currentNode.GetComponent<BreakNode>().loopStartNode = this.gameObject;
                isBreaking = true;
                break;
            }

            //다음 노드 없으면 처음부터 실행
            while (currentNode.GetComponent<IFollowFlow>().NextFlow().isConnected)
            {
                //현재 노드의 Flow outPort 찾기
                currentFlowoutPort = currentNode.GetComponent<IFollowFlow>().NextFlow();
                //Flow outPort로 다음 node 찾아서 currentNode 업데이트
                currentNode = NextNode(currentFlowoutPort);

                if(currentNode.CompareTag("endNode"))
                {
                    Debug.Log("For 반복문 노드 끝 노드에 연결됨");
                    NodeManager.Instance.SetCompileError(true);
                    isBreaking = true;
                    break;
                }
                if (currentNode.CompareTag("Node_Break"))
                {
                    currentNode.GetComponent<BreakNode>().isForLoop = true;
                    currentNode.GetComponent<BreakNode>().loopStartNode = this.gameObject;
                    isBreaking = true;
                    break;
                }
                else yield return currentNode.GetComponent<INode>().Execute();
            }
            yield return null;
        }
    }

    IEnumerator INode.ProcessData()
    {
        yield return null;
    }
}
