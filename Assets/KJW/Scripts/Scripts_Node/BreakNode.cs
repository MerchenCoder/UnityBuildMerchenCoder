using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakNode : MonoBehaviour, INode, IFollowFlow
{
    //[NonSerialized] 
    public GameObject loopStartNode;
    //[NonSerialized] 
    public bool isWhileLoop;
    //[NonSerialized] 
    public bool isForLoop;

    private void Start()
    {
        isForLoop = false;
        isWhileLoop = false;
    }

    public IEnumerator Execute()
    {
        Debug.Log("break 실행");
        if (isWhileLoop)
        {
            loopStartNode.GetComponent<WhileNode_New>().isBreaking = true;
        }
        else if (isForLoop)
        {
            loopStartNode.GetComponent<ForLoopNode>().isBreaking = true;
        }

        yield return true;
    }

    public FlowoutPort NextFlow()
    {
        return null;
    }

    public IEnumerator ProcessData()
    {
        return null;
    }
}
