using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakNode_New : MonoBehaviour, INode, IFollowFlow
{
    public GameObject loopStartNode;
    public bool isWhileLoop;
    public bool isForLoop;

    private void Start()
    {
        this.GetComponent<NodeNameManager>().NodeName = "BreakNode";

        loopStartNode = null;
        isForLoop = false;
        isWhileLoop = false;

    }

    public IEnumerator Execute()
    {
        Debug.Log("break 실행");

        //반복문 내부가 아닌 곳에서 break를 사용한 경우(오류)
        if (loopStartNode == null)
        {
            Debug.Log("반복문이 아닌 곳에서 break 사용");
            NodeManager.Instance.SetCompileError(true, "반복문이 아닌 곳에서\nbreak를 사용할 수 없습니다.");
            Reset();
            yield break;
        }

        if (isWhileLoop)
        {
            loopStartNode.GetComponent<WhileNode_New>().isBreaking = true;
        }
        else if (isForLoop)
        {
            loopStartNode.GetComponent<ForLoopNode_New>().isBreaking = true;
        }

        //실행 후 모든 변수값 reset
        Reset();
        yield return true;
    }

    private void Reset()
    {
        loopStartNode = null;
        isWhileLoop = false;
        isForLoop = false;

    }

    public FlowoutPort NextFlow()
    {
        Debug.Log("Break 노드의 잘못된 사용");
        NodeManager.Instance.SetCompileError(true, "반복문이 아닌 곳에서\nbreak를 사용할 수 없습니다.");
        return null;
    }

    public IEnumerator ProcessData()
    {
        return null;
    }
}
