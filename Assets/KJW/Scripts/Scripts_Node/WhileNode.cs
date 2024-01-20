using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileNode : MonoBehaviour, INode, IFollowFlow
{
    private  Queue<INode> nodeForWhileQueue = new Queue<INode>();

    private int loopIndex;
    public int _index;
    private FlowoutPort loopFlowPort;
    private FlowoutPort endFlowProt;

    //실행할 INode 인스턴스들을 저장할 큐를 생성
    private Queue<INode> nodesToExecute = new Queue<INode>();

    void Start()
    {
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
            yield return new WaitForSeconds(1);
        }
    }


    // 실행함수 완전히 종료 후 종료플로우 따라서 ㄱㄱ
    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    IEnumerator INode.Execute()
    {
        throw new System.NotImplementedException();
    }
}
