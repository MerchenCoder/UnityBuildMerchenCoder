using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeManager : MonoBehaviour
{
    //--싱글톤 생성--//
    public static NodeManager Instance { get; private set; }


    //실행할 INode 인스턴스들을 저장할 큐를 생성
    private Queue<INode> nodesToExecute = new Queue<INode>();

    //변수 선언
    private GameObject startNode;
    private GameObject currentNode;
    private FlowoutPort currentFlowoutPort;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    //노드를 큐에 추가하는 메서드
    public void AddNodeToQueue(INode node)
    {
        nodesToExecute.Enqueue(node);
    }
    //큐에 있는 모든 노드를 실행하는 메서드
    public void ExecuteNodes()
    {
        StartCoroutine(ExecuteNodesInOrder());
    }


    private IEnumerator ExecuteNodesInOrder()
    {
        if (nodesToExecute.Count == 0)
        {
            Debug.Log("Empty Queue");
        }
        while (nodesToExecute.Count > 0)
        {
            //큐에서 노드를 하나씩 꺼낸다.
            INode currentNode = nodesToExecute.Dequeue();
            //노드를 실행한다.
            currentNode.Execute();

            //노드 실행 사이의 약간의 딜레이 주기
            yield return new WaitForSeconds(0.3f);
        }
    }


    //큐를 초기화하는 메서드
    public void ClearNodeQueue()
    {
        nodesToExecute.Clear();
    }




    public void Compile()
    {
        //큐 초기화
        ClearNodeQueue();
        //find start node
        startNode = GameObject.FindGameObjectWithTag("startNode");
        currentNode = startNode;
        try
        {
            while (currentNode.GetComponent<NodeNameManager>().NodeName != "EndNode")
            {
                Debug.Log("while문 수행");
                //현재 노드를 큐에 추가
                AddNodeToQueue(currentNode.GetComponent<INode>());
                //현재 노드의 Flow outPort 찾기
                currentFlowoutPort = currentNode.GetComponent<IFollowFlow>().NextFlow();
                Debug.Log("현재 플로우 아웃포트 " + currentFlowoutPort);
                //Flow outPort로 다음 node 찾아서 currentNode 업데이트
                currentNode = NextNode(currentFlowoutPort);

            }
            Debug.Log("Compile Complete");
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("Can't find start node / " + e.Message);
            Debug.LogError(e.StackTrace);
        }
    }



    public GameObject NextNode(FlowoutPort flowoutPort)
    {
        return flowoutPort.ConnectedPort.transform.parent.gameObject;
    }


}
