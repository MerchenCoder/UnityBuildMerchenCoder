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

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow_end").GetComponent<FlowoutPort>();
    }

    
}
