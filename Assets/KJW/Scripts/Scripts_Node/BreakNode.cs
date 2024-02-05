using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakNode : MonoBehaviour, INode, IFollowFlow
{
    [NonSerialized] public GameObject loopStartNode;
    [NonSerialized] public bool isWhileLoop;
    [NonSerialized] public bool isForLoop;

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
            loopStartNode.GetComponent<WhileNode>().isBreaking = true;
        }
        else if (isForLoop)
        {

        }

        yield return true;
    }

    public FlowoutPort NextFlow()
    {
        return null;
    }

    public IEnumerator ProcessData()
    {
        throw new NotImplementedException();
    }
}
