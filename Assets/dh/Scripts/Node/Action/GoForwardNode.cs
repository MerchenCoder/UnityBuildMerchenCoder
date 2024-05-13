using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForwardNode : MonoBehaviour, INode, IFollowFlow
{
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    //private GameObject playerActionBubble;
    private void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";

    }

    public IEnumerator Execute()
    {
        if (NodeManager.Instance.Mode != "run")
        {
            TestManager.Instance.playerOutput.Add(outputStr);
            yield break;
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        PlayerControl playerControl = player.GetComponent<PlayerControl>();
        playerControl.MoveToBlock(playerControl.forwardBlockPos);

    }

    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}
