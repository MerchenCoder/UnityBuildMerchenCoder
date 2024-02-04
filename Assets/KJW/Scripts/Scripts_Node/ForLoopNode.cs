using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForLoopNode : MonoBehaviour, INode, IFollowFlow
{
    private Queue<INode> nodeToExcute_save = new Queue<INode>();

    private int loopIndex;
    public int _index;
    [SerializeField] private DataInPort dataInPort;
    [SerializeField] private DataOutPort dataOutPort;

    //변수 선언
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;

    private void Start()
    {
        this.GetComponent<NodeNameManager>().NodeName = "ForLoopNode";
    }

    //노드를 큐에 추가하는 메서드
    public void AddNodeToQueue(INode node)
    {
        nodeToExcute_save.Enqueue(node);
    }

    //큐를 초기화하는 메서드
    public void ClearNodeQueue()
    {
        nodeToExcute_save.Clear();
    }


    //다음 노드 반환하는 메소드
    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        return flowoutPort.ConnectedPort.transform.parent.gameObject;
    }



    //컴파일
    public void Compile()
    {
        //큐 초기화
        ClearNodeQueue();
        try
        {
            //반복 시작 노드의 Flow outPort 찾기
            currentFlowoutPort = this.transform.Find("loopFlow").GetComponent<FlowoutPort>();
            //Flow loopPort로 반복내용 node 찾아서 currentNode 업데이트
            currentNode = NextNode(currentFlowoutPort);

            while (currentNode.GetComponent<NodeNameManager>().NodeName != "BreakNode")
            {
                //현재 노드를 큐에 추가
                AddNodeToQueue(currentNode.GetComponent<INode>());
                //다음 노드 없으면 탈출
                if (!currentNode.transform.Find("outFlow").GetComponent<FlowoutPort>().IsConnected) break;
                else
                {
                    //현재 노드의 Flow outPort 찾기
                    currentFlowoutPort = currentNode.GetComponent<IFollowFlow>().NextFlow();
                    //Flow outPort로 다음 node 찾아서 currentNode 업데이트
                    currentNode = NextNode(currentFlowoutPort);
                }
            }

            Debug.Log("(반복문) Compile Complete");
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.StackTrace);
        }
    }



    // 실행함수 완전히 종료 후 종료플로우
    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    IEnumerator INode.Execute()
    {
        _index = 0;
        // dataInPort에서 inputValue 가져오기
        loopIndex = dataInPort.InputValueInt;
        for (_index = 0; _index < loopIndex; ++_index)
        {
            Compile();
            Debug.Log(_index + 1 + "번째 실행");

            GetComponent<NodeData>().data_int = _index;
            if (dataOutPort.isConnected) dataOutPort.SendData();

            while (nodeToExcute_save.Count != 0)
            {
                //큐에서 노드 꺼내기
                INode currentNode = nodeToExcute_save.Dequeue();
                Debug.Log(currentNode);
                yield return currentNode.Execute();
            }
            yield return null;
        }
    }

    IEnumerator INode.ProcessData()
    {
        throw new NotImplementedException();
    }
}
