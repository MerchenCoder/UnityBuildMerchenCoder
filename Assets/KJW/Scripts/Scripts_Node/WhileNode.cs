using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhileNode : MonoBehaviour, INode, IFollowFlow
{
    private  Queue<INode> nodeForWhileQueue = new Queue<INode>();

    private bool loopCondition;
    public int _index;
<<<<<<< Updated upstream
    private FlowoutPort loopFlowPort;
    private FlowoutPort endFlowProt;
=======
    [SerializeField] private DataInPort dataInPort;
>>>>>>> Stashed changes

    //실행할 INode 인스턴스들을 저장할 큐를 생성
    private Queue<INode> nodesToExecute = new Queue<INode>();

    void Start()
    {
<<<<<<< Updated upstream
        _index = 0;
    }

    public void Execute()
    {
        // 큐에 반복내용 노드 찾아서 넣기
        // 0. dataInPort에서 inputValue 가져오기
        // 1. 조건 확인 (이건 For 노드에서만 하면 됨)
        //   2. 반복 내용으로 플로우 보내기
        //   2-1. loopIndex만큼 반복
        // 3. 종료 
=======
        this.GetComponent<NodeNameManager>().NodeName = "WhileNode";
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            Debug.Log("Empty Queue");
=======
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

                if(nodeToExcute_save.Count > 30)
                {
                    break;
                    Debug.Log("반복문 큐 꽉 참");
                }
            }

            Debug.Log("(반복문) Compile Complete");
>>>>>>> Stashed changes
        }
        while (nodesToExecute.Count > 0)
        {
            //큐에서 노드를 하나씩 꺼낸다.
            INode currentNode = nodesToExecute.Dequeue();
            //노드를 실행한다.
            currentNode.Execute();
            //노드 실행 사이의 약간의 딜레이 주기
            yield return new WaitForSeconds(1);
        }
    }


    // 실행함수 완전히 종료 후 종료플로우 따라서 ㄱㄱ
    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

<<<<<<< Updated upstream
    
=======
    IEnumerator INode.Execute()
    {
        if(dataInPort.connectedPort.transform.parent.GetComponent<GetValueNode>() != null)
        {
            dataInPort.connectedPort.transform.parent.GetComponent<GetValueNode>().BringValueData();
        }
        loopCondition = dataInPort.connectedPort.transform.parent.GetComponent<NodeData>().data_bool;

        for (_index = 0; loopCondition ; ++_index)
        {
            Compile();
            Debug.Log(_index + 1 + "번째 실행");

            while (nodeToExcute_save.Count != 0)
            {
                //큐에서 노드 꺼내기
                INode currentNode = nodeToExcute_save.Dequeue();
                Debug.Log(currentNode);
                yield return currentNode.Execute();
            }
            if (dataInPort.connectedPort.transform.parent.GetComponent<GetValueNode>() != null)
            {
                Debug.Log("변수연결");
                dataInPort.connectedPort.transform.parent.GetComponent<GetValueNode>().BringValueData();
            }
            loopCondition = dataInPort.InputValueBool;

            yield return null;
        }
    }
>>>>>>> Stashed changes
}
